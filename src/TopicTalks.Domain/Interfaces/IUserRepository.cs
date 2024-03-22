using TopicTalks.Domain.Entities;

namespace TopicTalks.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<bool> IsEmailExists(string email);
    Task<bool> IsUserExists(long? userId);
    Task<User?> GetAsync(string email, long roleId);
}