using System.Text.Json.Serialization;
using ControllerFluentValidation.Validators;
using FluentValidation;
using FluentValidation.AspNetCore; 
var builder = WebApplication.CreateBuilder(args);





builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>(); 


var app = builder.Build();

app.MapControllers();


app.Run();
