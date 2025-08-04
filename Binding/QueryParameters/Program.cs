using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/Books/{ID:int}/", (int ID, int Page, int PageSize) =>
{
    return Results
    .Ok(

        new
        {
            BookID = ID,
            Page = Page, 
            Size=PageSize
        }
    );
});

app.MapGet("/Date", () =>
{
    return Results.Ok(DateTime.Now.AddHours(-1));
});


app.Run();
