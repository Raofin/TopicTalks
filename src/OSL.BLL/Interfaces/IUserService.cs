using ErrorOr;
using OSL.BLL.Models;
using OSL.DAL.Entities;

namespace OSL.BLL.Interfaces;

public interface IUserService
{
    Task<bool> IsEmailUnique(string email);
    Task<ErrorOr<User>> RegisterUser(RegisterVM model);
}