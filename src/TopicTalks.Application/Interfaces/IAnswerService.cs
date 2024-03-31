using ErrorOr;
using TopicTalks.Application.Dtos;

namespace TopicTalks.Application.Interfaces;

public interface IAnswerService
{
    Task<AnswerResponseDto?> Create(AnswerRequestDto dto);
    Task<ErrorOr<AnswerResponseDto>> GetWithUserAsync(long questionId);
    Task<ErrorOr<AnswerResponseDto>> UpdateAsync(AnswerRequestDto dto);
    Task<ErrorOr<Success>> DeleteAsync(long answerId, string role, long userId);
    Task<bool> HasTeachersAnswer(int questionId);
    Task<List<AnswerWithRepliesDto>> AnswersWithReplies(long questionId, long parentAnswerId = 0);
}