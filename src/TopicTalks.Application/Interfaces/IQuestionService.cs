using ErrorOr;
using TopicTalks.Application.Dtos;
using TopicTalks.Domain.Enums;

namespace TopicTalks.Application.Interfaces;

public interface IQuestionService
{
    Task<QuestionResponseDto> CreateAsync(QuestionCreateDto dto, long userId);
    Task<List<QuestionResponseDto>> GetAsync();
    Task<ErrorOr<QuestionResponseDto>> GetAsync(long question);
    Task<ErrorOr<QuestionResponseDto>> GetWithUserAsync(long questionId);
    Task<List<QuestionResponseDto>> SearchAsync(string? searchText);
    Task<List<QuestionResponseDto>> GetByUserIdAsync(long userId);
    Task<List<QuestionResponseDto>> GetByUserResponsesAsync(long userId);
    Task<ErrorOr<QuestionWithAnswersDto>> GetWithAnswersAsync(long questionId);
    Task<ErrorOr<QuestionResponseDto>> UpdateAsync(QuestionUpdateDto dto, long userId, List<RoleType> roles);
    Task<ErrorOr<Success>> DeleteAsync(long questionId, long userId, List<RoleType> roles);
    Task<ErrorOr<byte[]>> GeneratePdfAsync(long questionId);
    Task<ErrorOr<Success>> UpdateNotificationAsync(long questionId, long userId);
}