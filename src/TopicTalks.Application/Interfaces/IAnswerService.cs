using ErrorOr;
using TopicTalks.Application.Dtos;

namespace TopicTalks.Application.Interfaces;

public interface IAnswerService
{
    Task<AnswerResponseDto?> Create(AnswerRequestDto dto);
    Task<ErrorOr<AnswerResponseDto>> GetWithUserAsync(long questionId);
    Task<ErrorOr<Success>> UpdateAsync(AnswerRequestDto dto);
    Task<ErrorOr<Success>> DeleteAsync(long answerId);
    Task<bool> HasTeachersAnswer(int questionId);
    Task<List<AnswerWithRepliesDto>> AnswersWithReplies(long questionId, long parentAnswerId = 0);
}