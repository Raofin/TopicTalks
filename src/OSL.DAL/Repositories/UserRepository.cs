using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ErrorOr;
using OSL.DAL.Entities;
using OSL.DAL.Interfaces;

namespace OSL.DAL.Repositories;

internal class UserRepository(OSLContext _dbContext) : IUserRepository
{
    public async Task<bool> IsEmailUnique(string email)
    {
        return !await _dbContext.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<ErrorOr<User>> Register(User user, UserRole userRole)
    {
        using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();

                var userRoleEntity = new UserRole {
                    UserId = user.UserId,
                    RoleId = userRole.RoleId
                };

                _dbContext.UserRoles.Add(userRoleEntity);
                await _dbContext.SaveChangesAsync();

                transaction.Commit();

                return user;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return Error.Failure($"Error: {ex.Message}");
            }
        }
    }
}