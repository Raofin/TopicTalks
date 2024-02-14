using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ErrorOr;
using OSL.DAL.Entities;
using OSL.DAL.Interfaces;

namespace OSL.DAL.Repositories;

internal class UserRepository(OslDbContext _dbContext) : IUserRepository
{
    public async Task<bool> IsEmailExists(string email)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> IsUserExists(long? userId)
    {
        return await _dbContext.Users.AnyAsync(u => u.UserId == userId);
    }

    public async Task<ErrorOr<User>> Register(User user, UserRole userRole, UserDetail? userDetail)
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

                if (userDetail?.Name is not null)
                {
                    userDetail.UserId = user.UserId;
                    _dbContext.UserDetails.Add(userDetail);
                    await _dbContext.SaveChangesAsync();
                }

                transaction.Commit();

                return user;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return Error.Unexpected(description: ex.Message);
            }
        }
    }

    public async Task<ErrorOr<User>> Get(string email, long roleId)
    {
        try
        {
            var user = await _dbContext.Users
                        .Include(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                        .Where(u => u.Email == email && u.UserRoles.Any(ur => ur.RoleId == roleId))
                        .FirstOrDefaultAsync();

            return user is null
                ? Error.NotFound()
                : user;
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: ex.Message);
        }
    }

    public async Task<ErrorOr<User>> Get(long? userId)
    {
        try
        {
            var user = await _dbContext.Users
                        .Where(u => u.UserId == userId)
                        .FirstOrDefaultAsync();

            return user is null
                ? Error.NotFound()
                : user;
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: ex.Message);
        }
    }
}