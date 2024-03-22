using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TopicTalks.Domain.Enums;

namespace TopicTalks.Domain.Models;

public class LoginVM
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Role is required.")]
    public RoleType Role { get; set; }
}