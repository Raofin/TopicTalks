using ErrorOr;
using OSL.BLL.Interfaces;
using OSL.BLL.Models;
using OSL.DAL.Entities;
using OSL.DAL.Interfaces;

namespace OSL.BLL.Services;

internal class UserService(IPasswordHashService _passwordHashService, IUserRepository _userRepository) : IUserService
{
    public async Task<bool> IsEmailExists(string email)
    {
        return await _userRepository.IsEmailExists(email);
    }

    public async Task<bool> IsUserExists(long userId)
    {
        return await _userRepository.IsUserExists(userId);
    }

    public async Task<ErrorOr<User>> RegisterUser(RegisterVM model)
    {
        try
        {
            if (await IsEmailExists(model.Email))
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

    public async Task<ErrorOr<User>> Login(LoginVM model)
    {
        try
        {
            var user = await _userRepository.Login(model.Email, (long)model.Role);

            if (user.IsError)
            {
                return user;
            }
            else if (user.Value is null || !_passwordHashService.VerifyPassword(user.Value.PasswordHash, user.Value.Salt, model.Password))
            {
                return Error.Unauthorized();
            }

            return user;
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }
}
