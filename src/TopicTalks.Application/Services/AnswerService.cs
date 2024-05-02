using ErrorOr;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Extensions;
using TopicTalks.Application.Interfaces;
using TopicTalks.Domain;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Enums;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Application.Services;

internal class AnswerService(IUnitOfWork unitOfWork, IEmailSender emailSender) : IAnswerService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmailSender _emailSender = emailSender;

    public async Task<ErrorOr<AnswerResponseDto>> CreateAsync(AnswerCreateDto dto, long userId)
    {
        var isQuestionOrParentExists = await _unitOfWork.Answer.IsQuestionOrParentExistsAsync(dto.QuestionId, dto.ParentAnswerId);

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

        _unitOfWork.Answer.Add(answer);
        await _unitOfWork.CommitAsync();

        var answerWithDetails = await _unitOfWork.Answer.GetWithUserAsync(answer.AnswerId);
        var question = answerWithDetails!.Question;

        if (answerWithDetails.IsNotified && question.User is not null && question.User.UserId != answer.UserId)
        {
            _emailSender.SendAnswerNotification(
                question.User.Email, 
                answerWithDetails.User!.Username,
                answerWithDetails.Explanation, 
                question.Explanation);
        }

        return answerWithDetails.ToDto();
    }

    public async Task<ErrorOr<AnswerResponseDto>> GetWithUserAsync(long answerId)
    {
        var answer = await _unitOfWork.Answer.GetWithUserAsync(answerId);

        return answer is null 
            ? Error.NotFound()
            : answer.ToDto();
    }

    public async Task<ErrorOr<AnswerResponseDto>> UpdateAsync(AnswerUpdateDto dto, List<RoleType> roles, long userId)
    {
        var answer = await _unitOfWork.Answer.GetAsync(dto.AnswerId);

        if (answer is null)
        {
            return Error.NotFound();
        }

        if (answer.UserId != userId && !roles.Contains(RoleType.Moderator))
        {
            return Error.Unauthorized();
        }

        answer.Explanation = dto.Explanation;

        await _unitOfWork.CommitAsync();

        return answer.ToDto();
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long answerId, List<RoleType> roles, long userId)
    {
        var answer = await _unitOfWork.Answer.GetAsync(answerId);

        if (answer is null)
        {
            return Error.NotFound();
        }

        if (answer.UserId != userId && !roles.Contains(RoleType.Moderator))
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
                UserInfo = ans.User?.ToBasicInfoDto(),
                Answers = CreateReplyDtos(answers, ans.AnswerId)
            }).ToList();
    }
}
