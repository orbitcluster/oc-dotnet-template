using Microsoft.AspNetCore.Mvc;

namespace OcDotnetTemplate.Controllers;

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

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
    [HttpGet("vulnerable")]
    public IEnumerable<WeatherForecast> GetVulnerable(string user_input)
    {
        // INTENTIONAL VULNERABILITY FOR SAST TESTING: CWE-89 (SQL Injection)
        // CodeQL will detect this string concatenation into a query.
        string query = "SELECT * FROM Weather WHERE City = '" + user_input + "'";
        Console.WriteLine("Executing query: " + query); 

        return Enumerable.Range(1, 1).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
            TemperatureC = 25,
            Summary = "Vulnerable"
        })
        .ToArray();
    }
}
