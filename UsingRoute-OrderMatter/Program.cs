var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();



app.Use(async (contex, next) =>
{
    var result = contex.GetEndpoint().DisplayName ?? "No End point Found";
    Console.WriteLine("Middleware 01: => " + result);  
    await next();
});




app.UseRouting();


app.Use(async (contex, next) =>
{
    var result = contex.GetEndpoint().DisplayName ?? "No End point Found";
    Console.WriteLine("Middleware 02: => " + result);  
    await next();
});



app.MapGet("/Products/1/", () =>
{
    return new
    {
        ID = 1,
        Name = "koka", 
        Price = 250
    };
});



app.Run();



