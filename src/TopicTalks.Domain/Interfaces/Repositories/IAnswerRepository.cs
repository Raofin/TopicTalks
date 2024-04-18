using TopicTalks.Domain.Entities;

namespace TopicTalks.Domain.Interfaces.Repositories;

public interface IAnswerRepository : IRepository<Answer>
{
    Task<Answer?> GetWithUserAsync(long answerId);
    Task<List<Answer>> GetParentAnswersAsync(long questionId, long parentAnswerId);
    Task<List<Answer>> GetByQuestionAsync(long questionId);
    Task<Answer?> GetRepliesAsync(long questionId, long answerId, long parentAnswerId);
    Task<bool> HasTeachersAnswerAsync(int questionId);
    Task<bool> IsQuestionOrParentExistsAsync(long questionId, long? parentAnswerId);
}