using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Infrastructure.Services;

internal class HashPassword(IPasswordHasher<IdentityUser> passwordHasher) : IHashPassword
{
    private readonly IPasswordHasher<IdentityUser> _passwordHasher = passwordHasher;

    public (string hashedPassword, string salt) HashPasswordWithSalt(string password)
    {
        var salt = GenerateSalt();
        var combinedPassword = password + salt;
        var hashedPassword = _passwordHasher.HashPassword(new IdentityUser(), combinedPassword);

        return (hashedPassword, salt);
    }

    public bool VerifyPassword(string hashedPasswordFromDatabase, string saltFromDatabase, string rawPassword)
    {
        var combinedPassword = rawPassword + saltFromDatabase;
        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(new IdentityUser(), hashedPasswordFromDatabase, combinedPassword);

        return passwordVerificationResult == PasswordVerificationResult.Success;
    }

    private string GenerateSalt()
    {
        var saltBytes = new byte[32];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }

        return Convert.ToBase64String(saltBytes);
    }
}
