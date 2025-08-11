

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    ///options.Filters.Add<TimeTrackerFilter>();
});   
var app = builder.Build();

app.MapControllers();

app.Run();
