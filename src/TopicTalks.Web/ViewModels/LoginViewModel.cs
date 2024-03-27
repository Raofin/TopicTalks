using System.ComponentModel.DataAnnotations;
using TopicTalks.Web.Enums;

namespace TopicTalks.Web.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Role is required.")]
    public RoleType Role { get; set; }
}

public record LoginResponse(
    long UserId,
    string Token,
    string Email,
    List<RoleType?> Role
);