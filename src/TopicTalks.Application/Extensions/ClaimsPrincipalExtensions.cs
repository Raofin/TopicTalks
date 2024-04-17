using System.Security.Claims;
using TopicTalks.Domain.Enums;

namespace TopicTalks.Application.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static long GetUserId(this ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst("UserId");

        if (userIdClaim != null && long.TryParse(userIdClaim.Value, out var userId))
        {
            return userId;
        }

        throw new Exception("UserId claim not found or invalid.");
    }

    public static string GetEmail(this ClaimsPrincipal user)
    {
        var emailClaim = user.FindFirstValue(ClaimTypes.Email);

        if (!string.IsNullOrEmpty(emailClaim))
        {
            return emailClaim;
        }

        throw new Exception("Email claim not found.");
    }

    public static List<RoleType> GetRoles(this ClaimsPrincipal user)
    {
        var roleClaims = user.FindAll(ClaimTypes.Role).ToList();

        if (roleClaims.Any())
        {
            return roleClaims.Select(c => Enum.Parse<RoleType>(c.Value)).ToList();
        }

        throw new Exception("Role claims not found");
    }
}