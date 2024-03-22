using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using TopicTalks.Application.Interfaces;
using TopicTalks.Domain.Interfaces;

namespace TopicTalks.Application.Services;

internal class PasswordHashService : IPasswordHashService
{
    public (string hashedPassword, string salt) HashPasswordWithSalt(string password)
    {
        var salt = GenerateSalt();
        var combinedPassword = $"{password}{salt}";
        var passwordHasher = new PasswordHasher<IdentityUser>();
        var hashedPassword = passwordHasher.HashPassword(new IdentityUser(), combinedPassword);

        return (hashedPassword, salt);
    }

    public bool VerifyPassword(string hashedPasswordFromDatabase, string saltFromDatabase, string providedPassword)
    {
        var combinedPassword = $"{providedPassword}{saltFromDatabase}";
        var passwordHasher = new PasswordHasher<IdentityUser>();
        var passwordVerificationResult = passwordHasher.VerifyHashedPassword(new IdentityUser(), hashedPasswordFromDatabase, combinedPassword);

        return passwordVerificationResult == PasswordVerificationResult.Success;
    }

    private string GenerateSalt()
    {
        byte[] saltBytes = new byte[32];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }

        return Convert.ToBase64String(saltBytes);
    }
}
