using TopicTalks.Domain.Entities;

namespace TopicTalks.Domain.Interfaces.Repositories;

public interface IQuestionRepository : IRepository<Question>
{
    Task<List<Question>> SearchAsync(string? searchText);
    Task<List<Question>> GetWithUserAsync();
    Task<Question?> GetWithUserAsync(long questionId);
    Task<List<Question>> GetByUserIdAsync(long userId);
    Task<Question?> GetByUserIdAsync(long questionId, long userId);
    Task<List<Question>> GetByUserResponsesAsync(long userId);
    Task<Question?> GetWithAnswersAsync(long questionId);
}