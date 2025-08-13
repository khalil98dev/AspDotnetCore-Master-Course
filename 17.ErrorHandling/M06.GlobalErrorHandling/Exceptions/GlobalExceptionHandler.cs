using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace M06.GlobalErrorHandling.Excepetions;


public class GlobalExceptionHandler(IProblemDetailsService problemDetailService
                                    ,IHostEnvironment environment
                                    ) : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {

        httpContext.Response.StatusCode = exception switch
        {
            ValidationException => StatusCodes.Status400BadRequest, 
            _=>StatusCodes.Status500InternalServerError 
        };

        return problemDetailService.TryWriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception , 

                ProblemDetails = new ProblemDetails
                {
                    Type = exception.GetType().Name,
                    Title = "Error Occured.",
                    Detail = (!environment.IsDevelopment()) ? exception.Message : string.Join(exception.Message, exception.StackTrace)
                }
            }
        ); 
    }
}