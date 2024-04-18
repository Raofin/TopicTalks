namespace TopicTalks.Domain.Interfaces.Core;

public interface IHashPassword
{
    (string hashedPassword, string salt) HashPasswordWithSalt(string password);
    bool VerifyPassword(string hashedPasswordFromDatabase, string saltFromDatabase, string rawPassword);
}