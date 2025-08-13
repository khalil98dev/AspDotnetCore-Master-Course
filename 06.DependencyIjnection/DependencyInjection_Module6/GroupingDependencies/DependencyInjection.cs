
namespace Microsoft.Extensions.DependencyInjection; 
public static class WeatherServices
{
    public static IServiceCollection AddWeatherServices(this IServiceCollection Services)
    {
        Services.AddTransient<IWeatherClient, WeatherClient>(); 
        Services.AddTransient<IWeatherService, WeatherService>();

        return Services; 

    }    
}