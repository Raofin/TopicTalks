using Microsoft.EntityFrameworkCore.ChangeTracking;
using TopicTalks.Domain.Interfaces;

namespace TopicTalks.Domain;

public interface IUnitOfWork : IDisposable
{
    IUserRepository User { get; }
    IAnswerRepository Answer { get; }
    IQuestionRepository Question { get; }
    IOtpRepository Otp { get; }

    Task<int> CommitAsync();
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}