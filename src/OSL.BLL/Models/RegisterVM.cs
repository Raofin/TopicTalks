using OSL.BLL.Enums;
using System.ComponentModel.DataAnnotations;

namespace OSL.BLL.Models;

public class RegisterVM
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
}