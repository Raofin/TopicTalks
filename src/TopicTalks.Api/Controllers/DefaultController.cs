using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TopicTalks.Api.Controllers;

[ApiController]
[AllowAnonymous]
public class DefaultController : ControllerBase
{
    [HttpGet("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Index()
    {
        return Redirect("/swagger/index.html");
    }
}