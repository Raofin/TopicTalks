using ErrorOr;
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

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginVM model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Error"] = "Please fill out all the fields properly.";
            return View(model);
        }

        var login = await _userService.Login(model);

        if (!login.IsError)
        {
            HttpContext.Session.SetString("UserId", login.Value.UserId.ToString());
            HttpContext.Session.SetString("Email", login.Value.Email);
            HttpContext.Session.SetString("Role", model.Role.ToString());

            return RedirectToAction("index", "home");
        }
        else if (login.Errors.Any(e => e.Type is ErrorType.Unauthorized))
        {
            ViewData["Error"] = "Invalid credentials";
        }
        else
        {
            ViewData["Error"] = login.FirstError.Description ?? "An error occurred";
        }

        return View(model);
    }
}
