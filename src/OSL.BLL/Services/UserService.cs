using ErrorOr;
using OSL.BLL.Interfaces;
using OSL.BLL.Models;
using OSL.DAL.Entities;
using OSL.DAL.Interfaces;

namespace OSL.BLL.Services;

internal class UserService(IPasswordHashService _passwordHashService, IUserRepository _userRepository) : IUserService
{
    public async Task<ErrorOr<User>> RegisterUser(RegisterVM model)
    {
        try
        {
            if (!await IsEmailUnique(model.Email))
            {
                return Error.Conflict("User with the provided email already exists.");
            }

            (string hashedPassword, string salt) = _passwordHashService.HashPasswordWithSalt(model.Password);

            var user = new User {
                Email = model.Email,
                PasswordHash = hashedPassword,
                Salt = salt
            };

            var userRole = new UserRole {
                RoleId = (long)model.Role,
            };

            return await _userRepository.Register(user, userRole);
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }

    public async Task<bool> IsEmailUnique(string email)
    {
        return await _userRepository.IsEmailUnique(email);
    }
}
