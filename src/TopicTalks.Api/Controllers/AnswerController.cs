using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using TopicTalks.Api.Attributes;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Extensions;
using TopicTalks.Application.Interfaces;
using TopicTalks.Domain.Enums;

namespace TopicTalks.Api.Controllers;

public class AnswerController(IAnswerService answerService) : ApiController
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
        var answerDto = await _answerService.Create(dto, User.GetUserId());

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
        var answerDto = await _answerService.UpdateAsync(dto, User.GetRoles(), User.GetUserId());

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
        var result = await _answerService.DeleteAsync(answerId, User.GetRoles(), User.GetUserId());

        return !result.IsError
            ? Ok()
            : result.FirstError.Type switch {
                ErrorType.NotFound => NotFound("Answer was not found."),
                ErrorType.Unauthorized => Unauthorized("You are not authorized to delete this answer."),
                _ => Problem("An unexpected error occurred.")
            };
    }
}