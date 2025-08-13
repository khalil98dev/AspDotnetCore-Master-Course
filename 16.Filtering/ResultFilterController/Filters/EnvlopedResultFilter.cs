using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ResultFilterController.Filters;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class EnvlopedResultFilter : Attribute, IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is ObjectResult objectResult)
        {
            var response = new
            {
                Success = true,
                Data = objectResult.Value,
                Message = "Request processed successfully."
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = objectResult.StatusCode
            };
        }
        else if (context.Result is NotFoundResult)
        {
            context.Result = new ObjectResult(new
            {
                Success = false,
                Message = "Resource not found."
            })
            {
                StatusCode = 404
            };
        }

        await next();
    }
}
