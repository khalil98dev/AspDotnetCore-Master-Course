using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ResultFilter.Filters.EnvlopedResultMinimalFilter;

public class EnvlopedResultMinimalFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var result = await next(context);

        if (result is not ProblemDetails )
        {
          
            return new
            {
                success = true,
                data = result,
                message = "Operation completed successfully."
            };
        }
        return result;
    }
}