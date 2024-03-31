using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
    public async Task<IActionResult> Answer(long answerId)
    {
        var answer = await _answerService.GetWithUserAsync(answerId);

        return answer.IsError
            ? NotFound()
            : Ok(answer.Value);
    }

    [Authorize(Roles = nameof(RoleType.Student) + "," + nameof(RoleType.Teacher))]
    [HttpPost]
    public async Task<IActionResult> PostAnswer(AnswerRequestDto dto)
    {
        var answer = await _answerService.Create(dto);

        return Ok(answer);
    }

    [Authorize]
    [HttpPatch]
    public async Task<IActionResult> UpdateAnswer(AnswerRequestDto dto)
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
    public async Task<IActionResult> DeleteAnswer(long answerId)
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