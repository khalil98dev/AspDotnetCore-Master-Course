using ResourceFilter.Filters;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(
    options =>
    {
        options.Filters.Add<AuthoriseFilter>();
    }
);



var app = builder.Build();

app.MapControllers();   

app.Run();
