var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();




//Midllewares Goes here => 

//Principle Methode (Take ReqeustDelegate => return RequestDeleaget)
app.Use((RequestDelegate next) =>
{
    return async (HttpContext contex) =>
    {
        await contex.Response.WriteAsync("test middleware with organize methode");
        await next(contex);
    };
});




//Exstention Methode for use 
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("--#MD1 before ");
    await next(context);
    await context.Response.WriteAsync("--#MD1 after   ");

});

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("--#MD1 before ");
    await next(context); 
    await context.Response.WriteAsync("--#MD1 after   ");

});


app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("\n  --#MD2 before ");
    await next(context);
    await context.Response.WriteAsync("\n  --#MD2 after   ");

});

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("\n     --#MD3 before");
    await next(context); 
    await context.Response.WriteAsync("\n      --#MD3 after");
});

//Middleware With Run(); 

app.Run(async contex =>
{
    await contex.Response.WriteAsync("the end of the pipeline middleware (terminal middleware)");
});

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("\n     --#MD3 before");
    await next(context);
    await context.Response.WriteAsync("\n      --#MD3 after");
});


app.Run();
