using System.Reflection.Metadata.Ecma335;

var builder = WebApplication.CreateBuilder(args);

    builder.Configuration.AddJsonFile("myCustomCinfig.json",
                                        optional: false,
                                        reloadOnChange: true
                                      );

builder.Configuration.AddIniFile("Cong.ini",
                                        optional: false,
                                        reloadOnChange: true
                                      );

var app = builder.Build();

app.MapGet("/{key}", (string key, IConfiguration Config) =>

{
    return Config[key];
}
);

app.MapGet("/iniConfig/{key}", (string key, IConfiguration Config) =>

{
    return Config[key]; 
}
);

app.Run();
