using M01.DefaultBehaviour.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddProblemDetails(); 

var app = builder.Build();


app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();


app.MapControllers();


app.Run();
