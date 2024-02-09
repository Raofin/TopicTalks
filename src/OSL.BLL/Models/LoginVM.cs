using OSL.BLL.Enums;
using System.ComponentModel.DataAnnotations;

namespace OSL.BLL.Models;

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