using TopicTalks.Domain.Entities;

namespace TopicTalks.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<bool> IsEmailExists(string email);
    Task<User> GetByEmailAsync(string email);
    Task<List<User>> GetWithDetailsAsync();
    Task<User?> GetWithDetailsAsync(string email, long roleId);
    Task<User?> GetWithDetailsAsync(long userId);
}