namespace TopicTalks.Domain.Entities;

public class UserDetail
{
    public long UserDetailsId { get; set; }
    public long? UserId { get; set; }
    public string Name { get; set; } = null!;
    public string InstituteName { get; set; } = null!;
    public string IdCardNumber { get; set; } = null!;

    public User User { get; set; } = null!;
}