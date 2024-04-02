using Microsoft.EntityFrameworkCore.ChangeTracking;
using TopicTalks.Domain;
using TopicTalks.Domain.Interfaces;

namespace TopicTalks.Infrastructure.Persistence;

internal class UnitOfWork(
    AppDbContext dbContext,
    IUserRepository userRepository,
    IAnswerRepository answerRepository,
    IQuestionRepository questionRepository) : IUnitOfWork
{
    private readonly AppDbContext _context = dbContext;

    public IUserRepository User { get; } = userRepository;
    public IAnswerRepository Answer { get; } = answerRepository;
    public IQuestionRepository Question { get; } = questionRepository;

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
    {
        return _context.Entry(entity);
    }
}