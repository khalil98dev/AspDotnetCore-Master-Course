using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ResultFilter.Filters;

public class EnvlopedResultMinimalFilter : IEndpointFilter
{
    {

        var result = await next(context);   

        if (result is ObjectResult objectResult && objectResult.Value is not null)
        {
            // If the result is an ObjectResult, we can wrap it
            var wrappedResult = new
            {
                Success = true,
                Data = objectResult.Value
            };

            return new JsonResult(wrappedResult)
            {
                StatusCode = objectResult.StatusCode
            };
        }
        else if (result is null)
        {
            // If there is no result, we can set a default response
            return new ObjectResult(new { Success = true, Data = "No content" })
            {
                StatusCode = 204 // No Content
            };
        }   


    }
}