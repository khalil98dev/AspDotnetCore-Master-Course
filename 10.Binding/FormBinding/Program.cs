var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var app = builder.Build();

app.MapControllers(); 

app.MapPost("minimal-Invoice", async (IFormFile file) =>
{
    if (file is null || file.Length == 0)
        return Results.BadRequest("Uploaded Faild!");

    var Dir = Path.Combine(Directory.GetCurrentDirectory(),"Uploaded");

    if (!Directory.Exists(Dir))
    {
        Directory.CreateDirectory(Dir); 
    }

    var path = Path.Combine(Dir,"invoice",Path.GetExtension(file.FileName));

    using var stream = new FileStream(path, FileMode.Create);

    await file.CopyToAsync(stream); 

    return Results.Ok("Uploaded");
}).DisableAntiforgery();

app.Run();
