using EFCoreBasicsImplementation.Data;
using EndPoints;
using Microsoft.EntityFrameworkCore;
using ResultFilter.Filters;
using ResultFilter.Filters.EnvlopedResultMinimalFilter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductAppContext>(options =>
{
    options.UseSqlite("Data Source=products.db");
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<EnvelopedResultFilter>(); // Register the filter globally
});



var app = builder.Build();



app.MapProductEndpoints().AddEndpointFilter<EnvlopedResultMinimalFilter>(); // Register the filter for specific endpoints 

app.Run();
