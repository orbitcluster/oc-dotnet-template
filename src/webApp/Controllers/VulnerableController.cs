using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace OcDotnetTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class VulnerableController : ControllerBase
{
    private readonly ILogger<VulnerableController> _logger;

    public VulnerableController(ILogger<VulnerableController> logger)
    {
        _logger = logger;
    }

    // VULNERABILITY 1: SQL Injection - User input directly concatenated into SQL query
    [HttpGet("user/{userId}")]
    public IActionResult GetUser(string userId)
    {
        // This is vulnerable to SQL injection!
        string query = "SELECT * FROM Users WHERE Id = '" + userId + "'";
        _logger.LogInformation("Executing query: " + query);
        
        // Simulated - no actual DB connection
        return Ok(new { Query = query, Message = "This endpoint has SQL injection vulnerability" });
    }

    // VULNERABILITY 2: Hardcoded credentials
    [HttpGet("connect")]
    public IActionResult ConnectToDatabase()
    {
        // Hardcoded credentials - security vulnerability!
        string username = "admin";
        string password = "SuperSecret123!";
        string connectionString = $"Server=myserver;Database=mydb;User={username};Password={password}";
        
        return Ok(new { ConnectionString = connectionString });
    }

    // VULNERABILITY 3: Path Traversal
    [HttpGet("file")]
    public IActionResult GetFile([FromQuery] string filename)
    {
        // Path traversal vulnerability - user can access files outside intended directory
        string path = Path.Combine("/var/app/files", filename);
        
        if (System.IO.File.Exists(path))
        {
            return Ok(new { Path = path });
        }
        return NotFound();
    }

    // VULNERABILITY 4: Weak cryptography
    [HttpGet("hash")]
    public IActionResult HashPassword([FromQuery] string password)
    {
        // Using weak MD5 hash algorithm
        using var md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
        byte[] hashBytes = md5.ComputeHash(inputBytes);
        
        return Ok(new { Hash = Convert.ToHexString(hashBytes) });
    }
}
