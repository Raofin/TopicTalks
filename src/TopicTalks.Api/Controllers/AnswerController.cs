using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TopicTalks.Api.Attributes;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Interfaces;
using TopicTalks.Domain.Enums;

namespace TopicTalks.Api.Controllers;

[Route("api/answer")]
[ApiController]
public class AnswerController(IAnswerService answerService) : ControllerBase
{
    private readonly IAnswerService _answerService = answerService;

    [Authorize]
    [HttpGet("{answerId}")]
    public async Task<IActionResult> Get(long answerId)
    {
        var answer = await _answerService.GetWithUserAsync(answerId);

        return answer.IsError
            ? NotFound()
            : Ok(answer.Value);
    }

    [AuthorizeRoles(RoleType.Student, RoleType.Teacher)]
    [HttpPost]
    public async Task<IActionResult> Create(AnswerRequestDto dto)
    {
        var answer = await _answerService.Create(dto);

        return Ok(answer);
    }

    [Authorize]
    [HttpPatch]
    public async Task<IActionResult> Update(AnswerRequestDto dto)
    {
        var response = await _answerService.UpdateAsync(dto);

        if (response.IsError is false)
            return Ok(response.Value);
        
        return response.FirstError.Type == ErrorType.NotFound 
            ? NotFound() 
            : Problem();
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Delete(long answerId)
    {
        var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var userRole = User.FindFirst(ClaimTypes.Role)!.Value;

        var response = await _answerService.DeleteAsync(answerId, userRole, userId);

        return response.IsError is false
            ? Ok()
            : response.FirstError.Type switch {
                ErrorType.NotFound => NotFound(),
                ErrorType.Unauthorized => Unauthorized(),
                _ => Problem()
            };
    }
}