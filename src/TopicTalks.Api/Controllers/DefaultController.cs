using Microsoft.AspNetCore.Mvc;

namespace TopicTalks.Api.Controllers;

[ApiController]
public class DefaultController : ControllerBase
{
    [HttpGet("Hello")]
    public IActionResult HelloWorld()
    {
        return Ok("Hello World");
    }

    [HttpGet("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Index()
    {
        return Redirect("/swagger/index.html");
    }
}
