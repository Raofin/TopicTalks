using System.ComponentModel.DataAnnotations;
using TopicTalks.Web.Entities;
using TopicTalks.Web.Enums;

namespace TopicTalks.Web.ViewModels;

public class RegisterViewModel
{
    [Required]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Address")]
    public required string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public required string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Role is required.")]
    public RoleType Role { get; set; }

    public UserDetail? UserDetail { get; set; }
}