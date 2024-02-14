using ErrorOr;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OSL.BLL.Interfaces;
using OSL.BLL.Models;
using OSL.BLL.Services;

namespace OSL.WEB.Controllers;

public class UserController(IUserService _userService, IAuthService _authService, IHttpContextAccessor _httpAccessor) : Controller
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

    [Authorize]
    [HttpGet("get")]
    public IActionResult Gett()
    {
        return Ok(_authService.UserEmail);
    }


    [HttpGet("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        if (_httpAccessor.HttpContext != null)
        {
            await _httpAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        return RedirectToAction("index", "home");
    }
}