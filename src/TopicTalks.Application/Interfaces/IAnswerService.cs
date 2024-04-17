using ErrorOr;
using TopicTalks.Application.Dtos;
using TopicTalks.Domain.Enums;

namespace TopicTalks.Application.Interfaces;

public interface IAnswerService
{
    Task<ErrorOr<AnswerResponseDto>> Create(AnswerCreateDto dto, long userId);
    Task<ErrorOr<AnswerResponseDto>> GetWithUserAsync(long answerId);
    Task<ErrorOr<AnswerResponseDto>> UpdateAsync(AnswerUpdateDto dto, List<RoleType> roles, long userId);
    Task<ErrorOr<Success>> DeleteAsync(long answerId, List<RoleType> roles, long userId);
    Task<List<AnswerWithRepliesDto>> GetAnswersWithRepliesAsync(long questionId);
}