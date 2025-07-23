using Microsoft.AspNetCore.Mvc;
using PracticeBuddy.Core.DataAccess;

namespace PracticeBuddy.API.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController(ILogger<HealthController> logger, IDapperDb connection) : ControllerBase
{
    [HttpGet]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [EndpointDescription("Used by loadbalancers to figure out if server is alive")]	
    public async Task<IActionResult> GetHealthAsync()
    {
        logger.LogInformation("Health Check Requests");
        if (await connection.PingAsync()) return Ok("Database is OK!");

        return StatusCode(StatusCodes.Status500InternalServerError, "database unreachable");
    }
}