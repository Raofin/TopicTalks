using ErrorOr;
using TopicTalks.Application.Dtos;

namespace TopicTalks.Application.Interfaces;

public interface IQuestionService
{
    Task<QuestionResponseDto> CreateAsync(QuestionDto dto);
    Task<List<QuestionResponseDto>> GetAsync();
    Task<ErrorOr<QuestionResponseDto>> GetAsync(long question);
    Task<ErrorOr<QuestionResponseDto>> GetWithUserAsync(long questionId);
    Task<List<QuestionResponseDto>> SearchAsync(string? searchText);
    Task<List<QuestionResponseDto>> GetByUserIdAsync(long userId);
    Task<List<QuestionResponseDto>> GetByUserResponsesAsync(long userId);
    Task<ErrorOr<QuestionWithAnswersDto>> GetWithAnswersAsync(long questionId);
    Task<ErrorOr<QuestionResponseDto>> UpdateAsync(QuestionDto dto);
    Task<ErrorOr<Success>> DeleteAsync(long questionId, string role, long userId);
}