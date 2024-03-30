using TopicTalks.Domain;
using TopicTalks.Domain.Interfaces;

namespace TopicTalks.Infrastructure.Persistence;

internal class UnitOfWork(
    AppDbContext dbContext, 
    IUserRepository userRepository, 
    IAnswerRepository answerRepository) : IUnitOfWork
{
    private readonly AppDbContext _context = dbContext;

    public IUserRepository User { get; } = userRepository;
    public IAnswerRepository Answer { get; } = answerRepository;

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}