using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Microsoft.VisualBasic;

var builder = WebApplication.CreateBuilder(args);



var app = builder.Build();


void CommonBranch(IApplicationBuilder app)
{
    app.Use(async (context, next) =>
    {
        await context.Response.WriteAsync("MD1...\n");
        await next();
    });

    app.Use(async (context, next) =>
    {
        await context.Response.WriteAsync("MD2...\n");
        await next();
    });
};

void Branch1(IApplicationBuilder app)
{
    CommonBranch(app);

    app.Use(async (context, next) =>
    {
        await context.Response.WriteAsync("MD3...\n");
        await next();
    });

    app.Use(async (context, next) =>
    {
        await context.Response.WriteAsync("MD4...\n");
        await next();
    });
};

void Branch2(IApplicationBuilder app)
{
     CommonBranch(app);
    app.Use(async (context, next) =>
    {
        await context.Response.WriteAsync("MD5...\n");
        await next();
    });

    app.Use(async (context, next) =>
    {
        await context.Response.WriteAsync("MD6...\n");
        await next();
    });
};


app.Map("/Branch1", Branch1);

app.Map("/Branch2", Branch2);


app.MapWhen((contex) =>
{
    var config = builder.Configuration.GetValue<string>("User-role");
    return contex.Request.Path.Equals("/Checkout", StringComparison.OrdinalIgnoreCase) &&
     contex.Request.Query["role"] == config;
},
Branch1
);

app.Use(async (contex, next) =>

{
    await contex.Response.WriteAsync("Main Middleware");
    await next();
}

);


app.Run();
