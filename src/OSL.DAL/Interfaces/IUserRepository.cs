using ErrorOr;
using OSL.DAL.Entities;

namespace OSL.DAL.Interfaces;

public interface IUserRepository
{
    Task<bool> IsEmailExists(string email);
    Task<ErrorOr<User>> Login(string email, long roleId);
    Task<ErrorOr<User>> Register(User user, UserRole userRole);
}