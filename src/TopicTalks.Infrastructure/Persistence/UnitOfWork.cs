using TopicTalks.Domain;
using TopicTalks.Domain.Interfaces;

namespace TopicTalks.Infrastructure.Persistence;

internal class UnitOfWork(AppDbContext dbContext, IUserRepository userRepository) : IUnitOfWork
{
    private readonly AppDbContext _context = dbContext;

    public IUserRepository User { get; } = userRepository;

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task Dispose()
    {
        await _context.DisposeAsync();
    }
}