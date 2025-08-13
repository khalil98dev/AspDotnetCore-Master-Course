using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ResultFilter.Filters;

public class EnvelopedResultFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {

       if(context.Result is ObjectResult objectResult && objectResult.Value is not null )

        {
            // If the result is an ObjectResult, we can wrap it
            var wrappedResult = new
            {
                Success = true,
                Data = objectResult.Value
            };

            context.Result = new JsonResult(wrappedResult)
            {
                StatusCode = objectResult.StatusCode
            };
        }
        else if (context.Result is null)
        {
            // If there is no result, we can set a default response
            context.Result = new ObjectResult(new { Success = true, Data = "No content" })
            {
                StatusCode = 204 // No Content
            };
        }

      


        await next();   
    }
}