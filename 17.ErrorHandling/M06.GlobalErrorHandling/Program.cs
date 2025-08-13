using M01.DefaultBehaviour.Endpoints;
using M06.GlobalErrorHandling.Excepetions;




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails(opt =>
{
    opt.CustomizeProblemDetails = (context) =>
    {
        context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}"; 
        context.ProblemDetails.Extensions.Add("requestID", context.HttpContext.TraceIdentifier);
    };
});

var app = builder.Build();

app.UseExceptionHandler();

app.UseStatusCodePages();





app.MapControllers();

app.MapErrorEndpoints();

app.Run();
