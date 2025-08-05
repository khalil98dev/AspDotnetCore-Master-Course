using System.Text.Json.Serialization;
using Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();


builder.Services.AddControllers()
.AddNewtonsoftJson(options => 
{
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
})

.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
}); 



var app = builder.Build();

app.MapControllers();


app.Run();
