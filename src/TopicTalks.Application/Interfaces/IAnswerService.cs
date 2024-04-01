using ErrorOr;
using TopicTalks.Application.Dtos;

namespace TopicTalks.Application.Interfaces;

public interface IAnswerService
{
    Task<AnswerResponseDto?> Create(AnswerDto dto);
    Task<ErrorOr<AnswerResponseDto>> GetWithUserAsync(long answerId);
    Task<ErrorOr<AnswerResponseDto>> UpdateAsync(AnswerUpdateRequestDto dto);
    Task<ErrorOr<Success>> DeleteAsync(long answerId, string role, long userId);
    Task<bool> HasTeachersAnswer(int questionId);
    Task<List<AnswerWithRepliesDto>> GetAnswersWithRepliesAsync(long questionId, long parentAnswerId = 0);
}