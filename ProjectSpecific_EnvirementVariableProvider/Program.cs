var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/{key}", (string Key, IConfiguration config) =>
{
    return config[Key]; 
}
);

app.Run();
