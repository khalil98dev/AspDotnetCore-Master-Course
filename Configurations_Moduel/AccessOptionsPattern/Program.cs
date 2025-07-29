using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", 
    optional: false, 
    reloadOnChange: true);


// builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(AppSettings.Name))

builder.Services.AddOptions<AppSettings>().Bind(builder.Configuration.GetSection(AppSettings.Name)).Configure<IConfiguration>((settings, config) =>
    {
        config.GetSection(AppSettings.Name).Bind(settings);
    });



var app = builder.Build();

app.MapGet("/getOptiions", (IOptions<AppSettings> options) =>
{
    return options.Value;
});


app.MapGet("/getOptions-SnapShot", (IOptionsSnapshot<AppSettings> options) =>
{
    return options.Value;
});


app.MapGet("/getOptions-Monitor", (IOptionsMonitor<AppSettings> options) =>
{

     options.OnChange((settings) =>
    {
        Console.WriteLine($"Configuration changed ");
    });
    
    return options.CurrentValue;
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
