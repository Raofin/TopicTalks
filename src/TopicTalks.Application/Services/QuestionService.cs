using ErrorOr;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Extensions;
using TopicTalks.Application.Interfaces;
using TopicTalks.Domain;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Enums;

namespace TopicTalks.Application.Services;

internal class QuestionService(IUnitOfWork unitOfWork, IAnswerService answerService) : IQuestionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAnswerService _answerService = answerService;

    public async Task<QuestionResponseDto> CreateAsync(QuestionDto dto)
    {
        var question = new Question {
            Topic = dto.Topic,
            Explanation = dto.Explanation,
            UserId = dto.UserId
        };

        await _unitOfWork.Question.AddAsync(question);
        await _unitOfWork.CommitAsync();

        var response = await GetWithUserAsync(question.QuestionId);

        return response.Value;
    }

    public async Task<List<QuestionResponseDto>> GetAsync()
    {
        var questions = await _unitOfWork.Question.GetWithUser();

        return questions.Select(q => q.ToDto()).ToList();
    }

    public async Task<ErrorOr<QuestionResponseDto>> GetWithUserAsync(long questionId)
    {
        var question = await _unitOfWork.Question.GetWithUser(questionId);

        return question is null ? Error.NotFound()
            : new QuestionResponseDto(
                QuestionId: question.QuestionId,
                Topic: question.Topic,
                Explanation: question.Explanation,
                UserInfo: question.User is null ? null : new UserBasicInfo(
                        UserId: question.User.UserId,
                        Email: question.User.Email
                    ),
                CreatedAt: question.CreatedAt,
                UpdatedAt: question.UpdatedAt
            );
    }

    public async Task<List<QuestionResponseDto>> SearchAsync(string? searchText)
    {
        var questions = await _unitOfWork.Question.SearchAsync(searchText?.Trim());

        var responseDtos = questions.Select(q => q.ToDto()).ToList();

        return responseDtos;
    }

    public async Task<List<QuestionResponseDto>> GetByUserIdAsync(long userId)
    {
        var questions = await _unitOfWork.Question.GetByUserId(userId);

        return questions.Select(q => q.ToDto()).ToList();
    }

    public async Task<List<QuestionResponseDto>> GetByUserResponsesAsync(long userId)
    {
        var questions = await _unitOfWork.Question.GetByUserResponses(userId);

        return questions.Select(q => q.ToDto()).ToList();
    }

    public async Task<ErrorOr<QuestionWithAnswersDto>> GetWithAnswersAsync(long questionId)
    {
        var question = await _unitOfWork.Question.GetWithAnswers(questionId);
        var answers = await _answerService.AnswersWithReplies(questionId);

        return question is null
            ? Error.NotFound()
            : new QuestionWithAnswersDto(
                QuestionId: question.QuestionId,
                Topic: question.Topic,
                Explanation: question.Explanation,
                HasTeachersResponse: question.Answers
                    .Any(answer => answer.User is not null && answer.User.UserRoles
                        .Any(ur => ur.Role.RoleName == nameof(RoleType.Teacher))),
                UserInfo: question.User is null
                    ? null
                    : new UserBasicInfo(question.User.UserId, question.User.Email),
                Answers: answers,
                CreatedAt: question.CreatedAt,
                UpdatedAt: question.UpdatedAt
            );
    }

    public async Task<ErrorOr<QuestionResponseDto>> UpdateAsync(QuestionDto dto)
    {
        var question = await _unitOfWork.Question.GetWithUser(dto.QuestionId);

        if (question is null)
        {
            return Error.NotFound();
        }

        question.Topic = dto.Topic;
        question.Explanation = dto.Explanation;
        question.UpdatedAt = DateTime.Now;

        await _unitOfWork.CommitAsync();

        return question.ToDto();
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long questionId, string role, long userId)
    {
        var question = await _unitOfWork.Question.GetAsync(questionId);

        if (question is null)
        {
            return Error.NotFound();
        }

        if (question.UserId != userId || role is not nameof(RoleType.Student) and not nameof(RoleType.Teacher))
        {
            return Error.Unauthorized();
        }

        _unitOfWork.Question.Remove(question);

        var deletes = await _unitOfWork.CommitAsync();

        return deletes == 0
            ? Error.Unexpected()
            : Result.Success;
    }
}
