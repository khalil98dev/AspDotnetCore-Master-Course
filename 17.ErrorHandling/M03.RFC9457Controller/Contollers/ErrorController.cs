using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace M02.DeveloperExceptionPage.Contollers;

public class ErrorController : ControllerBase
{

    [Route("/error")]
    public IActionResult Error()
    {
        var problemDetails = new ProblemDetails
        {
            Type="",
            Title = "",
            Detail = "Error Server when you ",
            Instance = HttpContext.Request.Path,
            Status =500,

        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };

    }





    [Route("/error-development")]
    public IActionResult HandleErrorDevelopment(
    [FromServices] IHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsDevelopment())
        {
            return NotFound();
        }

        var exception=
            HttpContext.Features.Get<IExceptionHandlerFeature>()!.Error;


        var problemDetails = new ProblemDetails
        {
            Type="",
            Title = exception?.Message?? "Unhandled message",
            Detail = exception?.StackTrace?? "No Stack Trace Found",
            Instance = HttpContext.Request.Path,
            Status = HttpContext.Response.StatusCode,

        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };

    }
}