using Microsoft.AspNetCore.Mvc;

namespace OSL.WEB.Controllers;

public class AuthController : Controller
{
    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }
}
