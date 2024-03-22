using TopicTalks.Domain.Interfaces;

namespace TopicTalks.Domain;

public interface IUnitOfWork
{
    IUserRepository User { get; }
    Task<int> Complete();
    Task Dispose();
}