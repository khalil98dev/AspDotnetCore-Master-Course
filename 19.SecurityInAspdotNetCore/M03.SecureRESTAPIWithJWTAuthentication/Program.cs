using System.Text;
using M03.SecureRESTAPIWithJWTAuthentication.Permissions;
using M03.SecureRESTAPIWithJWTAuthentication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<JwtServiceProvider>();
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 

}).AddJwtBearer(option =>
{
    var conf = builder.Configuration.GetSection("JwtSettings"); 

    option.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew=TimeSpan.Zero,
        ValidateIssuerSigningKey = true,
        ValidIssuer = conf["Issuer"],
        ValidAudience = conf["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["SecretKey"]!)),  
    };  
    
});


builder.Services.AddAuthorization(options =>
{
    //Projects
    options.AddPolicy(Permission.Project.Create, p => p.RequireClaim("permission", Permission.Project.Create));
    options.AddPolicy(Permission.Project.Read, p => p.RequireClaim("permission", Permission.Project.Read));
    options.AddPolicy(Permission.Project.Update, p => p.RequireClaim("permission", Permission.Project.Update));
    options.AddPolicy(Permission.Project.Delete, p => p.RequireClaim("permission", Permission.Project.Delete));
    options.AddPolicy(Permission.Project.AssignMember, p => p.RequireClaim("permission", Permission.Project.AssignMember));
    options.AddPolicy(Permission.Project.ManageBudget, p => p.RequireClaim("permission", Permission.Project.ManageBudget));

    //Tasks 
    options.AddPolicy(Permission.Task.Create, p => p.RequireClaim("permission", Permission.Task.Create));
    options.AddPolicy(Permission.Task.Read, p => p.RequireClaim("permission", Permission.Task.Read));
    options.AddPolicy(Permission.Task.Update, p => p.RequireClaim("permission", Permission.Task.Update));
    options.AddPolicy(Permission.Task.Delete, p => p.RequireClaim("permission", Permission.Task.Delete));
    options.AddPolicy(Permission.Task.AssignUser, p => p.RequireClaim("permission", Permission.Task.AssignUser));
    options.AddPolicy(Permission.Task.UpdateStatus, p => p.RequireClaim("permission", Permission.Task.UpdateStatus));
    options.AddPolicy(Permission.Task.Comment, p => p.RequireClaim("permission", Permission.Task.Comment));

    
}); 
var app = builder.Build();

app.MapControllers();


app.UseAuthentication();
app.UseAuthorization();
app.Run();
