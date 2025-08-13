
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();


app.UseRouting();



#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(ep =>
{
    ep.MapControllers();
    ep.MapGet("/Products", () =>
    {
        return new[]
            {
            "Product 01",
            "Product 02 ",
        };
    });
}
);
#pragma warning restore ASP0014 // Suggest using top level route registrations



app.MapGet("/Route-Table", (IServiceProvider sp)
=>
{
    var result = sp.GetRequiredService<EndpointDataSource>()
    .Endpoints.Select(ep => ep.DisplayName);

     return result; 
});



// Routign Matter
app.Run();



