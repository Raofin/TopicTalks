using Microsoft.EntityFrameworkCore;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Interfaces.Repositories;

namespace TopicTalks.Infrastructure.Persistence.Repositories;

internal class UserRepository(AppDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<bool> IsUserExistsAsync(string? username, string? email)
    {
        return await _dbContext.Users.AnyAsync(u =>
            (!string.IsNullOrEmpty(username) && u.Username == username) ||
            (!string.IsNullOrEmpty(email) && u.Email == email)
        );
    }

    public Task<User> GetByEmailAsync(string email)
    {
        return _dbContext.Users
            .Where(u => u.Email == email)
            .SingleAsync();
    }

    public async Task<List<User>> GetWithDetailsAsync()
    {
        return await _dbContext.Users
            .Include(u => u.UserDetails)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ToListAsync();
    }

    public async Task<User?> GetWithDetailsAsync(string usernameOrEmail)
    {
        return await _dbContext.Users
            .Include(u => u.UserDetails)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Where(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail)
            .SingleOrDefaultAsync();
    }

    public async Task<User?> GetWithDetailsAsync(long userId)
    {
        return await _dbContext.Users
            .Include(u => u.UserDetails)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Where(u => u.UserId == userId)
            .SingleOrDefaultAsync();
    }

    public async Task<User?> GetBasicInfoAsync(long userId)
    {
        return await _dbContext.Users
            .Include(u => u.ImageFile)
            .Where(u => u.UserId == userId)
            .SingleOrDefaultAsync();
    }
}