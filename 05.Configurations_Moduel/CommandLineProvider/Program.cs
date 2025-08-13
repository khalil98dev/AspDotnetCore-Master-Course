var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/{Key}", (string Key,IConfiguration Config) =>

{
    return Config[Key]; 
});

app.Run();
