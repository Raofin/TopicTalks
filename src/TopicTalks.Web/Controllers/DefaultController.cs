using Microsoft.AspNetCore.Mvc;

namespace TopicTalks.Web.Controllers;

public class DefaultController() : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [Route("401")]
    public IActionResult Unauthorized401()
    {
        return View();
    }
}
