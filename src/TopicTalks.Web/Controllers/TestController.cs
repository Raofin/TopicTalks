using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopicTalks.Web.Attributes;
using TopicTalks.Web.Enums;

namespace TopicTalks.Web.Controllers;

public class TestController : Controller
{
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("success");
    }

    [Authorize]
    [HttpGet("protected")]
    public IActionResult Protected()
    {
        return Ok("authorized");
    }

    [AuthorizeStudent]
    [HttpGet("protected2")]
    public IActionResult Protected2()
    {
        return Ok("authorized student");
    }

    [AuthorizeRoles(RoleType.Teacher, RoleType.Moderator)]
    [HttpGet("protected3")]
    public IActionResult Protected3()
    {
        return Ok("authorized teacher");
    }
}