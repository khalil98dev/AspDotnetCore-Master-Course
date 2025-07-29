using System.Reflection.Metadata.Ecma335;

var builder = WebApplication.CreateBuilder(args);

var Data = new SortedList<string, string?>()
{
    {"MaxConnections" ,"10"}
};


builder.Configuration.AddInMemoryCollection(Data);  

var app = builder.Build();

app.MapGet("/{key}", (string key, IConfiguration Config) =>

{
    return Config[key];
}
);


app.Run();
