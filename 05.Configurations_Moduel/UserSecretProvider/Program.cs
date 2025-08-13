var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/{key}", (string key,IConfiguration Config) =>
{
    return Config[key]; 
}

);

app.Run();
