using System.Text.Json.Serialization;
using MinimalFluentValidation.Requests;
using ControllerFluentValidation.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using MinimalFluentValidation.Filters;

// var builder = WebApplication.CreateBuilder(args);


// builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
// {
//     options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
// });


// builder.Services.AddFluentValidationAutoValidation();

// builder.Services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>(); 


// var app = builder.Build();


// app.MapPost("/api/products", ([FromBody]CreateProductRequest? request) =>
// {
//     return Results.Created($"/api/products/{Guid.NewGuid()}", request); 
// }).AddEndpointFilter<ValidationFilter<CreateProductRequest>>();



// app.Run();



var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();

var app = builder.Build();

app.MapPost("/api/products", (CreateProductRequest? request) =>
{
    return Results.Created($"/api/products/{Guid.NewGuid()}", request);
}).AddEndpointFilter<ValidationFilter<CreateProductRequest>>();

app.Run();
