using System.Security.Claims;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopicTalks.Api.Attributes;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Interfaces;
using TopicTalks.Domain.Enums;

namespace TopicTalks.Api.Controllers;

[Route("api/question")]
[ApiController]
public class QuestionController(IQuestionService questionService) : ControllerBase
{
    private readonly IQuestionService _questionService = questionService;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var questions = await _questionService.GetAsync();

        return Ok(questions);
    }

    [Authorize]
    [HttpGet("{questionId}")]
    public async Task<IActionResult> Get(long questionId)
    {
        var response = await _questionService.GetWithUserAsync(questionId);

        return response.IsError
            ? NotFound()
            : Ok(response.Value);
    }

    [Authorize]
    [HttpGet("withAnswers/{questionId}")]
    public async Task<IActionResult> GetWithAnswers(int questionId)
    {
        var question = await _questionService.GetWithAnswersAsync(questionId);

        return question.IsError
            ? NotFound()
            : Ok(question.Value);
    }

    [HttpGet("search/{query}")]
    public async Task<IActionResult> Search(string query = "")
    {
        var questions = await _questionService.SearchAsync(query);

        return Ok(questions);
    }

    [Authorize(Roles = nameof(RoleType.Student))]
    [HttpPost]
    public async Task<IActionResult> Create(QuestionRequestDto dto)
    {
        var response = await _questionService.CreateAsync(dto);

        return Ok(response);
    }

    [Authorize]
    [HttpPatch]
    public async Task<IActionResult> Update(QuestionRequestDto dto)
    {
        var response = await _questionService.UpdateAsync(dto);

        return response.IsError
            ? NotFound()
            : Ok(response.Value);
    }

    [Authorize]
    [HttpDelete("{questionId}")]
    public async Task<IActionResult> Delete(long questionId)
    {
        var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var userRole = User.FindFirst(ClaimTypes.Role)!.Value;

        var response = await _questionService.DeleteAsync(questionId, userRole, userId);

        return response.IsError is false
            ? Ok()
            : response.FirstError.Type switch {
                ErrorType.NotFound => NotFound(),
                ErrorType.Unauthorized => Unauthorized(),
                _ => Problem()
            };
    }

    [AuthorizeStudent]
    [HttpGet("currentUser/questions")]
    public async Task<IActionResult> GetCurrentUserQuestions()
    {
        var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var questions = await _questionService.GetByUserIdAsync(userId);

        return Ok(questions);
    }

    [AuthorizeTeacher]
    [HttpGet("currentUser/responses")]
    public async Task<IActionResult> GetCurrentUserResponses()
    {
        var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var questions = await _questionService.GetByUserResponsesAsync(userId);

        return Ok(questions);
    }
}