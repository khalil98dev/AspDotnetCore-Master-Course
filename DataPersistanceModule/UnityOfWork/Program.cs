using EFCoreBasicsImplementation.Data;
using EFCoreBasicsImplementation.interfaces;
using EFCoreBasicsImplementation.Repositories;
using EndPoints;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

builder.Services.AddDbContext<ProductAppContext>(options =>
{
    options.UseSqlite("Data Source=App.db");
}
);


var app = builder.Build();

app.MapProductEndpoints();


app.Run();
