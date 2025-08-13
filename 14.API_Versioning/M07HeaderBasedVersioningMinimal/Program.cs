using PrtoductsController.V2;
using PrtoductsController.V1;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning.Builder;
using Asp.Versioning;
using Date;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ProductRepository>();
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1.0);
    options.ApiVersionReader = new HeaderApiVersionReader("v");
});



var app = builder.Build();



var versionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1.0))
    .HasApiVersion(new ApiVersion(2.0))
    .ReportApiVersions()
    .Build();

    

app.MapProductEndpointsV1(versionSet); 
app.MapProductEndpointsV2(versionSet);




app.Run();
