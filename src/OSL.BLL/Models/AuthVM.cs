using System.ComponentModel.DataAnnotations;

namespace OSL.BLL.Models;

public enum UserRole
{
    Student,
    Teacher,
    Moderator
}

public class RegisterVM
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public required string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Role is required.")]
    public UserRole Role { get; set; }
}

public class LoginVM
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Role is required.")]
    public UserRole Role { get; set; }
}
