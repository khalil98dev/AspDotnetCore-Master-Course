using EFCoreBasicsImplementation.Data;
using EndPoints;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IProductRepository,ProductRepository>();


builder.Services.AddDbContext<ProductAppContext>(options =>
{
    options.UseSqlite("Data Source=App.db");
}
);




var app = builder.Build();

app.MapProductEndpoints();



app.Run();
