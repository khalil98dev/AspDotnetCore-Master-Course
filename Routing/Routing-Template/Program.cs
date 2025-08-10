using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/Products/{ID}", (int ID) => $"product : {ID}");

app.MapGet("/Date/{Year}-{Month}-{Day}", (int Year,int Month,int Day) => 
{
    return $"Date is {new DateOnly(Year,Month,Day)}"; 
});

//Default value 

app.MapGet("/{controller=Home}", (string? controller) => controller);

//Optional Parameter: 
string[] Users()
{
    return new[]
    {
        "Momamed kami",
        "Anis rouab",
        "Mustafa",
        "Chaoib",
        "khalil"
    };
}


app.Map("/Users/{ID?}", (int? ID) =>
{
    return (ID is not null)
        ? Results.Ok(new { userID = ID, UserName = "khalil98dev" })
        : Results.Ok(Users());
});

//single catch 
app.MapGet("/Single/{*sing}", (string sing) => sing);

//catchall
app.MapGet("/All/{**si}", (string si) => si);

//Compsit 
app.MapGet("/Composit/a{One}b{two}",(string one,string two) => $"a: { one} + b: {two}" );   

app.Run();
