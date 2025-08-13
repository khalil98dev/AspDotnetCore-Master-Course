var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

public class WeatherService
{

    public string GetCityTemp(string CityName) => new WeatherClient().GetCity(CityName);


}


public class WeatherClient
{
    public string GetCity(string CityName)
    {
        return $"Weather of  {CityName} is {Random.Shared.Next(1, 80)}"; 
    }
}