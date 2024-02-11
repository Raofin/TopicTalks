using OSL.DAL.Entities;

namespace OSL.DAL.Models;

public class RegistrationModel
{
    public User User { get; set; } = null!;
    public UserDetail UserDetail { get; set; } = null!;
}
