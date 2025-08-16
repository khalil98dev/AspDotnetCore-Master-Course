using DataProtection.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IBidService, BidService>();




builder.Services.AddDbContext<DataProtection.Data.AppContext>(options =>
{
    options.UseSqlite("data Source=app.db");
});



builder.Services.AddControllers();

var app = builder.Build();



app.MapControllers();



app.Run();
