using TopicTalks.Domain.Interfaces;

namespace TopicTalks.Domain;

public interface IUnitOfWork : IDisposable
{
    IUserRepository User { get; }
    IAnswerRepository Answer { get; }
    IQuestionRepository Question { get; }

    Task<int> CommitAsync();
}