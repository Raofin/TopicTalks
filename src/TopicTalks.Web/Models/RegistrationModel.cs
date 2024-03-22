using TopicTalks.Web.Entities;

namespace TopicTalks.Web.Models;

public class RegistrationModel
{
    public User User { get; set; } = null!;
    public UserDetail UserDetail { get; set; } = null!;
}
