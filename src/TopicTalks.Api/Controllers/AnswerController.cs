using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TopicTalks.Api.Attributes;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Interfaces;
using TopicTalks.Domain.Enums;

namespace TopicTalks.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/answer")]
public class AnswerController(IAnswerService answerService) : ControllerBase
{
    private readonly IAnswerService _answerService = answerService;

    [HttpGet("{answerId}")]
    public async Task<IActionResult> Get(long answerId)
    {
        var answerDto = await _answerService.GetWithUserAsync(answerId);

        return !answerDto.IsError
            ? Ok(answerDto.Value)
            : answerDto.FirstError.Type switch {
                ErrorType.NotFound => NotFound("Answer was not found."),
                _ => Problem("An unexpected error occurred.")
            };
    }

    [HttpGet("withReplies/{answerId}")]
    public async Task<IActionResult> GetAnswerWithReplies(long answerId)
    {
        var answerDto = await _answerService.GetAnswersWithRepliesAsync(answerId);

        return Ok(answerDto);
    }

    [AuthorizeRoles(RoleType.Student, RoleType.Teacher)]
    [HttpPost]
    public async Task<IActionResult> Create(AnswerCreateDto dto)
    {
        var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var answerDto = await _answerService.Create(dto, userId);

        return !answerDto.IsError
            ? Ok(answerDto.Value)
            : answerDto.FirstError.Type switch {
                ErrorType.NotFound => NotFound("Question or any parent answer was not found."),
                _ => Problem("An unexpected error occurred.")
            };
    }

    [HttpPatch]
    public async Task<IActionResult> Update(AnswerUpdateDto dto)
    {
        var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var userRole = User.FindFirst(ClaimTypes.Role)!.Value;

        var answerDto = await _answerService.UpdateAsync(dto, userRole, userId);

        return !answerDto.IsError
            ? Ok(answerDto.Value)
            : answerDto.FirstError.Type switch {
                ErrorType.NotFound => NotFound("Answer was not found."),
                ErrorType.Unauthorized => Unauthorized("You are not authorized to update this answer."),
                _ => Problem("An unexpected error occurred.")
            };
    }

    [HttpDelete("{answerId}")]
    public async Task<IActionResult> Delete(long answerId)
    {
        var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var userRole = User.FindFirst(ClaimTypes.Role)!.Value;

        var result = await _answerService.DeleteAsync(answerId, userRole, userId);

        return !result.IsError
            ? Ok()
            : result.FirstError.Type switch {
                ErrorType.NotFound => NotFound("Answer was not found."),
                ErrorType.Unauthorized => Unauthorized("You are not authorized to delete this answer."),
                _ => Problem("An unexpected error occurred.")
            };
    }
}