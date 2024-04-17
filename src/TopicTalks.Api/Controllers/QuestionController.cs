﻿using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopicTalks.Api.Attributes;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Extensions;
using TopicTalks.Application.Interfaces;

namespace TopicTalks.Api.Controllers;

[Route("api/question")]
[ApiController]
public class QuestionController(IQuestionService questionService) : ControllerBase
{
    private readonly IQuestionService _questionService = questionService;

    [HttpGet]
    public async Task<IActionResult> Get(string? searchQuery)
    {
        var questionDtos = await _questionService.SearchAsync(searchQuery);

        return Ok(questionDtos);
    }

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

    [Authorize]
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

    [Authorize]
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

    [Authorize]
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

    [Authorize]
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

    [HttpGet("pdf/{questionId}")]
    public async Task<IActionResult> GetPdf(long questionId)
    {
        var pdf = await _questionService.GeneratePdfAsync(questionId);

        return !pdf.IsError
            ? File(pdf.Value, "application/pdf")
            : pdf.FirstError.Type switch {
                ErrorType.NotFound => NotFound("Question was not found."),
                _ => Problem("An unexpected error occurred.")
            };
    }
}