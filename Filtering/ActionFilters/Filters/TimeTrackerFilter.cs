
using Microsoft.AspNetCore.Mvc.Filters;

namespace ActionFilters.Filters.TimeTrackerFilterV1;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class TimeTrackerFilter :Attribute ,IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine("Action Strated");

        var startTime = context.HttpContext.Items["StartTime"] = DateTime.UtcNow;

        await next();

        Console.WriteLine("Action Finished");

        var endTime = DateTime.Now;

        var duration = endTime - (DateTime)startTime;

        context.HttpContext.Response.Headers.Add("X-Action-Duration", duration.TotalMilliseconds.ToString());

        // Log the duration or do something with it
        Console.WriteLine($"Action executed in {duration.TotalMilliseconds} ms");
    }
} 
