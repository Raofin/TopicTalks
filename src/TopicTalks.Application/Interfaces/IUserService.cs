using ErrorOr;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Models;

namespace TopicTalks.Application.Interfaces;

public interface IUserService
{
    Task<ErrorOr<User>> Get(long? userId);
    Task<bool> IsEmailExists(string email);
    Task<ErrorOr<string>> Login(LoginVM model);
    Task<ErrorOr<User>> RegisterUser(RegisterVM model);
}