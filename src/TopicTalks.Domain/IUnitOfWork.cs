using TopicTalks.Domain.Interfaces;

namespace TopicTalks.Domain;

public interface IUnitOfWork : IDisposable
{
    IUserRepository User { get; }
    IAnswerRepository Answer { get; }

    Task<int> CommitAsync();
}