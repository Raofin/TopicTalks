using Microsoft.AspNetCore.Mvc;

namespace TopicTalks.Web.Controllers;

public class DefaultController() : Controller
{
    [Route("401")]
    public IActionResult Unauthorized401()
    {
        return View();
    }
}
