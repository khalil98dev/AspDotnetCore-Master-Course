var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddProductService();



var app = builder.Build();

app.UseRouting();

app.MapControllerRoute(
    name:"Default",
    pattern : "{controller=Products}/{action=index}/{id?}"
); 

app.Run();
