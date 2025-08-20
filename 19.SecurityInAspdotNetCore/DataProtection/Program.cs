using DataProtection.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IBidService, BidService>();

builder.Services.AddDataProtection()
                .PersistKeysToDbContext<DataProtection.Data.AppContext>();

builder.Services.Configure<JsonOptions>(opt =>
{
    opt.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull; 
});


builder.Services.AddDbContext<DataProtection.Data.AppContext>(options =>
{
    options.UseSqlite("data Source=app.db");
});



builder.Services.AddControllers();

var app = builder.Build();



app.MapControllers();



app.Run();
