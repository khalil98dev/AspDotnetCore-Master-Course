var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();




app.Use(async (context, next) =>
{
    
    context.Response.StatusCode = StatusCodes.Status201Created;
    context.Response.Headers.Append("koko","momo");
    await context.Response.WriteAsync("#MD3 after");
    await next();
});




app.Run();
