using Microsoft.AspNetCore.Mvc;

namespace OSL.WEB.Controllers;

public class HomeController : Controller
{
    [Route("/")]
    public IActionResult Index()
    {
        return View();
    }
}
