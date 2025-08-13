using M01.DefaultBehaviour.Endpoints;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddProblemDetails(); 



var app = builder.Build();


app.UseExceptionHandler();

app.UseDeveloperExceptionPage();

app.UseStatusCodePages();

app.MapErrorEndpoints();




app.Run();
