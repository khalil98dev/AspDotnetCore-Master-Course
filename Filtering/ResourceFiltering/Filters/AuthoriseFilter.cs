
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ResourceFilter.Filters;

public class AuthoriseFilter(IConfiguration Config) : IAsyncResourceFilter
{
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        var tenetID = context.HttpContext.Request.Headers["tenantId"].ToString();
        var apikey = context.HttpContext.Request.Headers["x-api-key"].ToString();

        var expectedApiKey = Config.GetSection("Tenants").GetSection(tenetID).GetValue<string>("ApiKey");  

        if(expectedApiKey == null || expectedApiKey != apikey)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        await next();   
    }
}