using Microsoft.AspNetCore.Mvc;
using OSL.BLL.Interfaces;
using OSL.BLL.Models;

namespace OSL.WEB.Controllers;

public class UserController(IUserService _userService) : Controller
{
    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterVM model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Error"] = "Please fill out all the fields properly.";
            return View(model);
        }

        var registration = await _userService.RegisterUser(model);

        if (registration.IsError)
        {
            ViewData["Error"] = registration.FirstError.Description;
            return View(model);
        }

        return RedirectToAction("login");
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }
}
