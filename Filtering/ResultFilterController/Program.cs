using Microsoft.Extensions.Options;
using ResultFilterController.Data.interfaces;
using ResultFilterController.Data.Repositories;
using ResultFilterController.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(static options =>
{
    options.Filters
    .Add<EnvlopedResultFilter>(); 
    options.Filters
    .Add<ExceptionsResultFilter>();
    
});

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddControllers();

var app = builder.Build();



app.MapControllers();   


app.Run();
