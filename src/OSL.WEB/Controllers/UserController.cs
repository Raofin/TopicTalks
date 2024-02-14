using ErrorOr;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using OSL.BLL.Interfaces;
using OSL.BLL.Models;
using OSL.WEB.Extensions;

namespace OSL.WEB.Controllers;

public class UserController(IUserService _userService, IHttpContextAccessor _httpAccessor) : Controller
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
            ModelState.ValidationFailed();
        }

        var registration = await _userService.RegisterUser(model);

        if (registration.Errors.Any(e => e.Type is ErrorType.Conflict))
        {
            return Conflict("User with the provided email already exists.");
        }
        else if (registration.IsError)
        {
            return BadRequest(registration.FirstError.Description ?? "An error occurred.");
        }

        return Ok();
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
            ModelState.ValidationFailed();
        }

        var login = await _userService.Login(model);

        if (login.Errors.Any(e => e.Type is ErrorType.NotFound or ErrorType.Unauthorized))
        {
            return Unauthorized("Invalid Credentials.");
        }
        else if (login.IsError)
        {
            return BadRequest(login.FirstError.Description ?? "An error occurred.");
        }
     
        return Ok();
    }

    [HttpGet("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        if (_httpAccessor.HttpContext != null)
        {
            await _httpAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        return RedirectToAction("index", "default");
    }
}