using System.Security.Claims;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopicTalks.Api.Attributes;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Interfaces;

namespace TopicTalks.Api.Controllers;

[Route("api/question")]
[ApiController]
public class QuestionController(IQuestionService questionService, IHttpContextAccessor accessor) : ControllerBase
{
    private readonly IQuestionService _questionService = questionService;

    [HttpGet]
    public async Task<IActionResult> Get(string? searchQuery)
    {
        var questions = await _questionService.SearchAsync(searchQuery);

        return Ok(questions);
    }

    [HttpGet("{questionId}")]
    public async Task<IActionResult> Get(long questionId)
    {
        var response = await _questionService.GetAsync(questionId);

        return response.IsError
            ? NotFound()
            : Ok(response.Value);
    }

    [Authorize]
    [HttpGet("withUserDetails/{questionId}")]
    public async Task<IActionResult> GetWithUserDetails(long questionId)
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

    [AuthorizeStudent]
    [HttpPost]
    public async Task<IActionResult> Create(QuestionRequestDto dto)
    {
        var question = new QuestionDto(
                QuestionId: 0,
                Topic: dto.Topic,
                Explanation: dto.Explanation,
                UserId: long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
            );

        var response = await _questionService.CreateAsync(question);

        return Ok(response);
    }

    [Authorize]
    [HttpPatch]
    public async Task<IActionResult> Update(QuestionUpdateRequestDto dto)
    {
        var question = new QuestionDto(
                QuestionId: dto.QuestionId, 
                Topic: dto.Topic, 
                Explanation: dto.Explanation, 
                UserId: long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
            );

        var response = await _questionService.UpdateAsync(question);

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