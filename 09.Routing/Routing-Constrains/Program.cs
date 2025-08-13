var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/Users/{ID:int}", (int ID) => $"User ID : {ID}");

app.Map("/Name/{Name:required}", (string Name) => $"Name: {Name}");

app.MapGet("/Guid/{UserID:guid}", (Guid UserID) => $"User Guid ID: {UserID}");

app.MapGet("/Date/{date:datetime}", (DateTime date) => $"Curretn Date is {date}");

app.MapGet("/Max/{name:maxlength(10)}", (string name) => $"max name{name}");



app.Run();
