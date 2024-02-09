using ErrorOr;
using OSL.DAL.Entities;

namespace OSL.DAL.Interfaces;

public interface IUserRepository
{
    Task<bool> IsEmailUnique(string email);
    Task<ErrorOr<User>> Register(User user, UserRole userRole);
}