using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using TopicTalks.Api.Extensions;
using TopicTalks.Application.Interfaces;
using TopicTalks.Domain.Interfaces;
using TopicTalks.Domain.Models;

namespace TopicTalks.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService _userService) : ControllerBase
{

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

        return Ok(login.Value);
    }
}
