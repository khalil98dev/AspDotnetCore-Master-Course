var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/UserName/{ID:int}", (int ID) =>
{
    return   (ID==1)? "Jolaibib abde bari bla ":"Not Found"; 
}); 

app.Run();


