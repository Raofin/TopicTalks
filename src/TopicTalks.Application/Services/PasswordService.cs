using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using TopicTalks.Application.Interfaces;

namespace TopicTalks.Application.Services;

internal class PasswordService(IPasswordHasher<IdentityUser> passwordHasher) : IPasswordService
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
