using Microsoft.AspNetCore.Mvc;

namespace OSL.WEB.Controllers;

public class HomeController() : Controller
{
    [Route("401")]
    public IActionResult Unauthorized401()
    {
        return View();
    }
}
