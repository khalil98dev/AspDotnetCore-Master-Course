using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



app.MapGet("/get-value-by-key", (IConfiguration Config) =>
{
    return Config["ServiceName"];
});


app.MapGet("/get-ConnectionString", (IConfiguration Config) =>
{
    return Config["ConnectionStrings:DefaultConnection"];
});


app.MapGet("/get-ConnectionStringByMethode", (IConfiguration Config) =>
{
    return Config.GetConnectionString("DefaultConnection");
});


app.MapGet("/get-SingleValueByMethode", (IConfiguration Config) =>
{
    return Config.GetValue<string>("ServiceName");
});


app.MapGet("/get-ObjectSection", (IConfiguration Config) =>
{
    return Config.GetSection(AppSettings.Name).Get<AppSettings>();
});


app.MapGet("/get-BindValues", (IConfiguration Config) =>
{

    AppSettings appS = new();
    Config.GetSection(AppSettings.Name).Bind(appS);
    return appS;
});



app.Run();


public class AppSettings
{
    static public string Name = "AppSettings";

    public string Author { get; set; }
    public DateTime OpenAt { get; set; }
    public DateTime  ClosedAt { get; set; }
    public string   Role  { get; set; }
}
