using ErrorOr;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Interfaces;
using TopicTalks.Domain;
using TopicTalks.Domain.Entities;

namespace TopicTalks.Application.Services;

internal class AnswerService(IUnitOfWork unitOfWork) : IAnswerService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<AnswerResponseDto?> Create(AnswerRequestDto dto)
    {
        var answer = new Answer {
            ParentAnswerId = dto.ParentAnswerId,
            QuestionId = dto.QuestionId,
            Explanation = dto.Explanation,
            UserId = dto.UserId
        };

        await _unitOfWork.Answer.AddAsync(answer);
        await _unitOfWork.CommitAsync();

        var response = await GetWithUserAsync(answer.AnswerId);

        return response.Value;
    }

    public async Task<ErrorOr<AnswerResponseDto>> GetWithUserAsync(long questionId)
    {
        var answer = await _unitOfWork.Answer.GetWithUserAsync(questionId);


        return answer is null ? Error.NotFound()
            : new AnswerResponseDto(
                AnswerId: answer.AnswerId,
                ParentAnswerId: answer.ParentAnswerId,
                QuestionId: answer.QuestionId,
                Explanation: answer.Explanation,
                CreatedAt: answer.CreatedAt,
                UserInfo: answer.User == null ? null
                    : new UserBasicInfo(
                        UserId: answer.User.UserId,
                        Email: answer.User.Email
                    )
                );
    }

    public async Task<ErrorOr<Success>> UpdateAsync(AnswerRequestDto dto)
    {
        var answer = new Answer {
            AnswerId = dto.AnswerId,
            Explanation = dto.Explanation
        };

        await _unitOfWork.Answer.AddAsync(answer);
        var updates = await _unitOfWork.CommitAsync();

        return updates == 0
            ? Error.NotFound()
            : Result.Success;
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long answerId)
    {
        await _unitOfWork.Answer.RemoveWithRepliesAsync(answerId);
        var deletes = await _unitOfWork.CommitAsync();

        return deletes == 0
            ? Error.NotFound()
            : Result.Success;
    }

    public async Task<bool> HasTeachersAnswer(int questionId)
    {
        return await _unitOfWork.Answer.HasTeachersAnswerAsync(questionId);
    }

    public async Task<List<AnswerWithRepliesDto>> AnswersWithReplies(long questionId, long parentAnswerId = 0)
    {
        var answers = await _unitOfWork.Answer.GetParentAnswersAsync(questionId, parentAnswerId);

        var dtos = answers.Select(ans => new AnswerWithRepliesDto {
            AnswerId = ans.AnswerId,
            ParentAnswerId = ans.ParentAnswerId,
            QuestionId = ans.QuestionId,
            Explanation = ans.Explanation,
            CreatedAt = ans.CreatedAt,
            UserInfo = ans.User == null ? null : new UserBasicInfo(
                UserId: ans.User.UserId,
                Email: ans.User.Email
            )
        }).ToList();

        foreach (var item in dtos)
        {
            var replies = await AnswersWithReplies(questionId, item.AnswerId);
            item.Answers = replies;
        }

        return dtos;
    }
}
