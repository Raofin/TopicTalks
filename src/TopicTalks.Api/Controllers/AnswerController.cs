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

    [HttpGet("{answerId}")]
    public async Task<IActionResult> Get(long answerId)
    {
        var response = await _answerService.GetWithUserAsync(answerId);

        if (response.IsError is false)
            return Ok(response.Value);

        return response.FirstError.Type == ErrorType.NotFound
            ? NotFound()
            : Problem();

    }

    [HttpGet("withReplies/{answerId}")]
    public async Task<IActionResult> GetAnswerWithReplies(long answerId)
    {
        var answer = await _answerService.GetAnswersWithRepliesAsync(answerId);

        return Ok(answer);
    }

    /*    [Authorize]
        [HttpGet("{answerId}")]
        public async Task<IActionResult> Get(long answerId)
        {
            var answer = await _answerService.GetWithUserAsync(answerId);

            return answer.IsError
                ? NotFound()
                : Ok(answer.Value);
        }*/

    [AuthorizeRoles(RoleType.Student, RoleType.Teacher)]
    [HttpPost]
    public async Task<IActionResult> Create(AnswerRequestDto dto)
    {
        var answer = new AnswerDto(
               AnswerId: 0,
               ParentAnswerId: dto.ParentAnswerId,
               QuestionId: dto.QuestionId,
               Explanation: dto.Explanation,
               UserId: long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
            );
        
        var response = await _answerService.Create(answer);

        return Ok(response);
    }

    [Authorize]
    [HttpPatch]
    public async Task<IActionResult> Update(AnswerUpdateRequestDto dto)
    {
        var response = await _answerService.UpdateAsync(dto);

        if (response.IsError is false)
            return Ok(response.Value);
        
        return response.FirstError.Type == ErrorType.NotFound 
            ? NotFound() 
            : Problem();
    }

    [Authorize]
    [HttpDelete("{answerId}")]
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