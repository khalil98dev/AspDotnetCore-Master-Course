using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// builder.Services.AddAuthentication("Cookies")
//                 .AddCookie(); 

// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//                 .AddCookie(); 
// builder.Services.AddAuthentication(opt =>
// {
//     opt.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme; 
// }).AddCookie();       

//Welcome


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication()
                .AddCookie();
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("Admin-Driver", p =>
    {
        p.RequireRole("Admin");
        p.RequireClaim("Driver-calss", "A");

    });

    option.AddPolicy("SuperVisor-Driver", p =>
    {
        p.RequireRole("SuperVisor");
        p.RequireClaim("Driver-calss", "B");
    });
}); 

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login",async  (context) =>
{
    var data = new List<Claim>
    {
        new ("userName","khalil98dev"),
        new ("Email","khalil98dev@gmail.com"),
        new ("sub",Guid.NewGuid().ToString()),
        new ("Driver-calss","B"),
        new (ClaimTypes.Role,"Admin"),
        new (ClaimTypes.Role,"SuperVisor")
    };

    var identity = new ClaimsIdentity(data, CookieAuthenticationDefaults.AuthenticationScheme);

    var principal = new ClaimsPrincipal(identity);

    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal); 
    
   // await context.Response.WriteAsync("Logged in successfully");
});


app.MapGet("/logOut", async (context) =>
{
    await context.SignOutAsync(); 
});


app.MapGet("/user",[Authorize] (HttpContext context) =>
{
    var Claims = context.User.Claims.Select(c => new { c.Type, c.Value });
        return Results.Ok(Claims);
});



app.MapGet("/secure", () =>
{
    return Results.Ok("secure page");
}).RequireAuthorization();

app.MapGet("/Account/login", () =>
{
    return Results.Ok("login page");
});

app.MapGet("/AdminOnly", () =>
{
    return Results.Ok("Admin Dashboad");
}).RequireAuthorization(a=>a.RequireRole("Admin"));


app.MapGet("/SuberVisorOnly", [Authorize(Roles ="Admin,SuperVisor")]() =>
{
    return Results.Ok("Suber-Visor Dashboad");
});

app.MapPost("/drivers/class-a", () =>
{
    return Results.Ok("Drivers with class A");
}).RequireAuthorization("Admin-Driver");


app.MapPost("/drivers/class-b", () =>
{
    return Results.Ok("Drivers with class A");
}).RequireAuthorization("SuperVisor-Driver");






app.Run();
