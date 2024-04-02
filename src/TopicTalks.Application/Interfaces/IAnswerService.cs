using ErrorOr;
using TopicTalks.Application.Dtos;

namespace TopicTalks.Application.Interfaces;

public interface IAnswerService
{
    Task<ErrorOr<AnswerResponseDto>> Create(AnswerCreateDto dto, long userId);
    Task<ErrorOr<AnswerResponseDto>> GetWithUserAsync(long answerId);
    Task<ErrorOr<AnswerResponseDto>> UpdateAsync(AnswerUpdateDto dto, string role, long userId);
    Task<ErrorOr<Success>> DeleteAsync(long answerId, string role, long userId);
    Task<bool> HasTeachersAnswer(int questionId);
    Task<List<AnswerWithRepliesDto>> GetAnswersWithRepliesAsync(long questionId, long parentAnswerId = 0);
}