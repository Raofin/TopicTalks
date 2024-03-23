using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Interfaces;

namespace TopicTalks.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
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
}
