using Microsoft.EntityFrameworkCore;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Interfaces.Repositories;

namespace TopicTalks.Infrastructure.Persistence.Repositories;

internal class UserRepository(AppDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<bool> IsEmailExists(string email)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email == email);
    }

    public Task<User> GetByEmailAsync(string email)
    {
        var user = _dbContext.Users
            .Where(u => u.Email == email)
            .SingleAsync();

        return user;
    }

    public async Task<List<User>> GetWithDetailsAsync()
    {
        var user = await _dbContext.Users
            .Include(u => u.UserDetails)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ToListAsync();

        return user;
    }

    public async Task<User?> GetWithDetailsAsync(string email, long roleId)
    {
        var user = await _dbContext.Users
            .Include(u => u.UserDetails)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Where(u => u.Email == email && u.UserRoles.Any(ur => ur.RoleId == roleId))
            .SingleOrDefaultAsync();

        return user;
    }

    public async Task<User?> GetWithDetailsAsync(long userId)
    {
        var user = await _dbContext.Users
            .Include(u => u.UserDetails)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Where(u => u.UserId == userId)
            .SingleOrDefaultAsync();

        return user;
    }
}