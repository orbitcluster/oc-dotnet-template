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
        // INTENTIONAL VULNERABILITY: CWE-89
        // We must use a Real SQL Sink for CodeQL to flag it.
        // Console.WriteLine is safe. SqlCommand.ExecuteReader is not.
        using (var connection = new System.Data.SqlClient.SqlConnection("Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;"))
        {
            connection.Open();
            // BAD: Concatenation
            string query = "SELECT * FROM Weather WHERE City = '" + user_input + "'";
            using (var command = new System.Data.SqlClient.SqlCommand(query, connection))
            {
                // This is the SINK
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read()) { /* ... */ }
                }
            }
        }

        return Enumerable.Range(1, 1).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
            TemperatureC = 25,
            Summary = "Vulnerable"
        })
        .ToArray();
    }
}
