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
        ValidateIssuerSigningKey = true,
        ValidIssuer = conf["Issuer"],
        ValidAudience = conf["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["SecretKey"]!)), 
        
    };  
    
});


builder.Services.AddAuthorization(); 
var app = builder.Build();

app.MapControllers();


app.UseAuthentication();
app.UseAuthorization();
app.Run();
