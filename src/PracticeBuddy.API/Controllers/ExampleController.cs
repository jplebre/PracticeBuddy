using Microsoft.AspNetCore.Mvc;

namespace PracticeBuddy.API.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
[Route("[controller]/")]
public class ExampleController : ControllerBase
{
    public ExampleController()
    {

    }

    [HttpGet]
    public IActionResult GetItem()
    {
        return Ok(new { Thing = "a thing" });
    }

    [HttpPost]
    public IActionResult PostItem([FromBody] object o)
    {
        return Ok(new { Server = "created a thing, see thing", Thing = o });
    }
}