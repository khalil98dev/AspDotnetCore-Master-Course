using Microsoft.AspNetCore.Mvc.Rendering;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGet("/", () => "Hello World!");

//MiddleWares goes here  

// Middleware follow the SRP principle and it recive RequestDelegate and return RequestDelegate
//tq: The RequestDelegate it a Delegate That thak a HttpContex object and return Task 



///Middleware that do nothing ... 
//app.Use((RequestDelegate req) => req);


app.Use((RequestDelegate next) =>
{
    return async (HttpContext contex) =>
    {
        await contex.Response.WriteAsync("02-Middleware write from contex of request using normal function");
        await next(contex);
    };
});


app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("03-Middleware write with HttpContex-RequestDelegate");
   // await next(context);
}
);

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("04-Middleware write with HttpContex-RequestDelegate");
    await next(context);
}
);




app.Map("/public", () => "Hello world"); 

app.Run();
