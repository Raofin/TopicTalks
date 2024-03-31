using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Interfaces;

namespace TopicTalks.Api.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationRequest request)
    {
        var user = await _userService.Register(request);

        return user.Errors.Any(e => e.Type is ErrorType.Conflict)
            ? Conflict("User with the provided email already exists.")
            : Ok(user.Value);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var login = await _userService.Login(request);

        return login.Errors.Any(e => e.Type is ErrorType.NotFound or ErrorType.Unauthorized)
            ? Unauthorized("Invalid Credentials.")
            : Ok(login.Value);
    }

    [Authorize]
    [HttpGet("details")]
    public async Task<IActionResult> GetUser()
    {
        var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var user = await _userService.GetAsync(userId);

        return user.Errors.Any(e => e.Type is ErrorType.NotFound)
            ? NotFound("User not found.")
            : Ok(user.Value);
    }
}
