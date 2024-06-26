﻿using ErrorOr;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Extensions;
using TopicTalks.Application.Interfaces;
using TopicTalks.Domain;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Enums;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Application.Services;

internal class QuestionService(IUnitOfWork unitOfWork, IAnswerService answerService) : IQuestionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAnswerService _answerService = answerService;

    public async Task<QuestionResponseDto> CreateAsync(QuestionCreateDto dto, long userId)
    {
        var question = new Question {
            Topic = dto.Topic,
            Explanation = dto.Explanation,
            ImageFileId = dto.ImageFileId,
            UserId = userId
        };

        _unitOfWork.Question.Add(question);
        await _unitOfWork.CommitAsync();

        var response = await GetWithUserAsync(question.QuestionId);

        return response.Value;
    }

    public async Task<List<QuestionResponseDto>> GetAsync()
    {
        var questions = await _unitOfWork.Question.GetWithUserAsync();

        return questions.Select(q => q.ToDto()).ToList();
    }

    public async Task<ErrorOr<QuestionResponseDto>> GetAsync(long questionId)
    {
        var question = await _unitOfWork.Question.GetAsync(questionId);
        return question is null
            ? Error.NotFound()
            : question.ToDto();
    }

    public async Task<ErrorOr<QuestionResponseDto>> GetWithUserAsync(long questionId)
    {
        var question = await _unitOfWork.Question.GetWithUserAsync(questionId);

        return question is null
            ? Error.NotFound()
            : question.ToDto();
    }

    public async Task<List<QuestionResponseDto>> SearchAsync(string? searchText)
    {
        var questions = await _unitOfWork.Question.SearchAsync(searchText?.Trim());

        var responseDtos = questions.Select(q => q.ToDto()).ToList();

        return responseDtos;
    }

    public async Task<List<QuestionResponseDto>> GetByUserIdAsync(long userId)
    {
        var questions = await _unitOfWork.Question.GetByUserIdAsync(userId);

        return questions.Select(q => q.ToDto()).ToList();
    }

    public async Task<List<QuestionResponseDto>> GetByUserResponsesAsync(long userId)
    {
        var questions = await _unitOfWork.Question.GetByUserResponsesAsync(userId);

        return questions.Select(q => q.ToDto()).ToList();
    }

    public async Task<ErrorOr<QuestionWithAnswersDto>> GetWithAnswersAsync(long questionId)
    {
        var question = await _unitOfWork.Question.GetWithAnswersAsync(questionId);
        var answers = await _answerService.GetAnswersWithRepliesAsync(questionId);

        return question is null
            ? Error.NotFound()
            : new QuestionWithAnswersDto(
                QuestionId: question.QuestionId,
                Topic: question.Topic,
                Explanation: question.Explanation,
                IsNotified: question.IsNotified,
                HasTeachersResponse: question.Answers
                    .Any(answer => answer.User is not null && answer.User.UserRoles
                        .Any(ur => ur.Role.RoleName == nameof(RoleType.Teacher))),
                UserInfo: question.User?.ToBasicInfoDto(),
                Answers: answers,
                CreatedAt: question.CreatedAt,
                UpdatedAt: question.UpdatedAt,
                ImageFile: question.ImageFile?.ToDto()
            );
    }

    public async Task<ErrorOr<QuestionResponseDto>> UpdateAsync(QuestionUpdateDto dto, long userId, List<RoleType> roles)
    {
        var question = await _unitOfWork.Question.GetWithUserAsync(dto.QuestionId);

        if (question is null)
        {
            return Error.NotFound();
        }

        if (question.UserId != userId && !roles.Contains(RoleType.Moderator))
        {
            return Error.Unauthorized();
        }

        question.Topic = dto.Topic;
        question.Explanation = dto.Explanation;
        question.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.CommitAsync();

        return question.ToDto();
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long questionId, long userId, List<RoleType> roles)
    {
        var question = await _unitOfWork.Question.GetAsync(questionId);

        if (question is null)
        {
            return Error.NotFound();
        }

        if (question.UserId != userId && !roles.Contains(RoleType.Moderator))
        {
            return Error.Unauthorized();
        }

        _unitOfWork.Question.Remove(question);

        var deletes = await _unitOfWork.CommitAsync();

        return deletes == 0
            ? Error.Unexpected()
            : Result.Success;
    }

    public async Task<ErrorOr<Success>> UpdateNotificationAsync(long questionId, long userId)
    {
        var question = await _unitOfWork.Question.GetByUserIdAsync(questionId, userId);

        if (question is null)
        {
            return Error.NotFound();
        }

        question.IsNotified = !question.IsNotified;
        await _unitOfWork.CommitAsync();

        return Result.Success;
    }
}
