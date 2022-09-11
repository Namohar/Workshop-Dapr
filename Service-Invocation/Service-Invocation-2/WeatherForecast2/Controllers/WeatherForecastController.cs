using Microsoft.AspNetCore.Mvc;
using Dapr.Client;
namespace Workshop2.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "ProxyWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        HttpClient client = DaprClient.CreateInvokeHttpClient("weather");
        var weatherForecasts =
            await client.GetFromJsonAsync<List<WeatherForecast>>("weatherforecast");

        return weatherForecasts;
    }
}

