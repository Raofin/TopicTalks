using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopicTalks.Application.Attributes;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Extensions;
using TopicTalks.Application.Interfaces;

namespace TopicTalks.Api.Controllers;

public class QuestionController(IQuestionService questionService) : ApiController
{
    private readonly IQuestionService _questionService = questionService;

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get(string? searchQuery)
    {
        var questionDtos = await _questionService.SearchAsync(searchQuery);

        return Ok(questionDtos);
    }

    [AllowAnonymous]
    [HttpGet("{questionId}")]
    public async Task<IActionResult> Get(long questionId)
    {
        var questionDto = await _questionService.GetAsync(questionId);

        return !questionDto.IsError
            ? Ok(questionDto.Value)
            : questionDto.FirstError.Type switch {
                ErrorType.NotFound => NotFound("Question was not found."),
                _ => Problem("An unexpected error occurred.")
            };
    }

    [HttpGet("withUserDetails/{questionId}")]
    public async Task<IActionResult> GetWithUserDetails(long questionId)
    {
        var questionDto = await _questionService.GetWithUserAsync(questionId);

        return !questionDto.IsError
            ? Ok(questionDto.Value)
            : questionDto.FirstError.Type switch {
                ErrorType.NotFound => NotFound("Question was not found."),
                _ => Problem("An unexpected error occurred.")
            };
    }

    [AllowAnonymous]
    [HttpGet("withAnswers/{questionId}")]
    public async Task<IActionResult> GetWithAnswers(int questionId)
    {
        var questionDto = await _questionService.GetWithAnswersAsync(questionId);

        return !questionDto.IsError
            ? Ok(questionDto.Value)
            : questionDto.FirstError.Type switch {
                ErrorType.NotFound => NotFound("Question was not found."),
                _ => Problem("An unexpected error occurred.")
            };
    }

    [AuthorizeStudent]
    [HttpPost]
    public async Task<IActionResult> Create(QuestionCreateDto dto)
    {
        var questionDto = await _questionService.CreateAsync(dto, User.GetUserId());

        return Ok(questionDto);
    }

    [HttpPatch]
    public async Task<IActionResult> Update(QuestionUpdateDto dto)
    {
        var questionDto = await _questionService.UpdateAsync(dto, User.GetUserId(), User.GetRoles());

        return !questionDto.IsError
            ? Ok(questionDto.Value)
            : questionDto.FirstError.Type switch {
                ErrorType.NotFound => NotFound("Question was not found."),
                ErrorType.Unauthorized => Unauthorized("You are not authorized to update this question."),
                _ => Problem("An unexpected error occurred.")
            };
    }

    [HttpDelete("{questionId}")]
    public async Task<IActionResult> Delete(long questionId)
    {
        var result = await _questionService.DeleteAsync(questionId, User.GetUserId(), User.GetRoles());

        return !result.IsError
            ? Ok()
            : result.FirstError.Type switch {
                ErrorType.NotFound => NotFound("Question was not found."),
                ErrorType.Unauthorized => Unauthorized("You are not authorized to delete this question."),
                _ => Problem("An unexpected error occurred.")
            };
    }

    [AuthorizeStudent]
    [HttpGet("currentUser/questions")]
    public async Task<IActionResult> GetCurrentUserQuestions()
    {
        var questionDtos = await _questionService.GetByUserIdAsync(User.GetUserId());

        return Ok(questionDtos);
    }

    [AuthorizeTeacher]
    [HttpGet("currentUser/responses")]
    public async Task<IActionResult> GetCurrentUserResponses()
    {
        var questionDtos = await _questionService.GetByUserResponsesAsync(User.GetUserId());

        return Ok(questionDtos);
    }

    [HttpPatch("{questionId}/notification")]
    public async Task<IActionResult> UpdateNotification(long questionId)
    {
        var result = await _questionService.UpdateNotificationAsync(questionId, User.GetUserId());

        return !result.IsError
            ? Ok()
            : result.FirstError.Type switch {
                ErrorType.NotFound => NotFound("Question was not found."),
                _ => Problem("An unexpected error occurred.")
            };
    }
}