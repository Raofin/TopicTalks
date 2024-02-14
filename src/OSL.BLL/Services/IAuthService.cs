using OSL.DAL.Entities;

namespace OSL.BLL.Services;

public interface IAuthService
{
    string UserEmail { get; }
    string UserId { get; }
    string UserRole { get; }

    Task Authenticate(User user);
}