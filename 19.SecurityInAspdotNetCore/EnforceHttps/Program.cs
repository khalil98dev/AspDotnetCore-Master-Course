using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpsRedirection(
    options=>
    {
        options.RedirectStatusCode = StatusCodes.Status301MovedPermanently; 
    }
); 


var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!");

app.Run();
