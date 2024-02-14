using Microsoft.AspNetCore.Mvc;

namespace OSL.WEB.Controllers;

public class DefaultController() : Controller
{
    [Route("401")]
    public IActionResult Unauthorized401()
    {
        return View();
    }
}
