using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace OcDotnetTemplate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VulnerableController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public VulnerableController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("insecure-logging")]
        public IActionResult InsecureLogging(string userInput)
        {
            // Vulnerability: Log Injection / Neutralization of CRLF
            _logger.LogInformation("User input: " + userInput);
            return Ok("Logged");
        }

        [HttpGet("hardcoded-password")]
        public IActionResult HardcodedPassword()
        {
            // Vulnerability: Hardcoded Credential
            var password = "superSecretHardcodedPassword123!";
            if (password == "admin") {
                return Ok("Access Granted");
            }
            return Unauthorized();
        }

        [HttpGet("weak-hashing")]
        public IActionResult WeakHashing(string input)
        {
            // Vulnerability: Weak Cryptographic Algorithm (MD5)
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Ok(BitConverter.ToString(hash));
            }
        }
    }
}
