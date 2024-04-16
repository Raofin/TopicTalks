using ErrorOr;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Extensions;
using TopicTalks.Application.Interfaces;
using TopicTalks.Domain;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Enums;

namespace TopicTalks.Application.Services;

internal class AnswerService(IUnitOfWork unitOfWork) : IAnswerService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<AnswerResponseDto>> Create(AnswerCreateDto dto, long userId)
    {
        var isQuestionOrParentExists = await _unitOfWork.Answer.IsQuestionOrParentExists(dto.QuestionId, dto.ParentAnswerId);

        if (!isQuestionOrParentExists)
        {
            return Error.NotFound();
        }

        var answer = new Answer {
            ParentAnswerId = dto.ParentAnswerId,
            QuestionId = dto.QuestionId,
            Explanation = dto.Explanation,
            UserId = userId
        };

        await _unitOfWork.Answer.AddAsync(answer);
        await _unitOfWork.CommitAsync();

        // explicitly loading the User navigation property
        await _unitOfWork.Entry(answer).Reference(a => a.User).LoadAsync();

        return answer.ToDto();
    }

    public async Task<ErrorOr<AnswerResponseDto>> GetWithUserAsync(long questionId)
    {
        var answer = await _unitOfWork.Answer.GetWithUserAsync(questionId);

        return answer is null 
            ? Error.NotFound()
            : answer.ToDto();
    }

    public async Task<ErrorOr<AnswerResponseDto>> UpdateAsync(AnswerUpdateDto dto, string role, long userId)
    {
        var answer = await _unitOfWork.Answer.GetAsync(dto.AnswerId);

        if (answer is null)
        {
            return Error.NotFound();
        }

        if (answer.UserId != userId && role is not nameof(RoleType.Moderator))
        {
            return Error.Unauthorized();
        }

        answer.Explanation = dto.Explanation;

        await _unitOfWork.CommitAsync();

        return answer.ToDto();
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long answerId, string role, long userId)
    {
        var answer = await _unitOfWork.Answer.GetAsync(answerId);

        if (answer is null)
        {
            return Error.NotFound();
        }

        if (answer.UserId != userId && role is not nameof(RoleType.Moderator))
        {
            return Error.Unauthorized();
        }

        _unitOfWork.Answer.Remove(answer);
        await _unitOfWork.CommitAsync();

        return Result.Success;
    }

    public async Task<List<AnswerWithRepliesDto>> GetAnswersWithRepliesAsync(long questionId)
    {
        var answers = await _unitOfWork.Answer.GetByQuestionAsync(questionId);
        return CreateReplyDtos(answers);
    }

    private List<AnswerWithRepliesDto> CreateReplyDtos(List<Answer> answers, long parentAnswerId = 0)
    {
        return answers
            .Where(a => a.ParentAnswerId == parentAnswerId)
            .Select(ans => new AnswerWithRepliesDto {
                AnswerId = ans.AnswerId,
                ParentAnswerId = ans.ParentAnswerId,
                Explanation = ans.Explanation,
                CreatedAt = ans.CreatedAt,
                UserInfo = ans.User == null ? null : new UserBasicInfoDto(ans.User.UserId, ans.User.Email),
                Answers = CreateReplyDtos(answers, ans.AnswerId)
            }).ToList();
    }
}
