namespace TopicTalks.Domain.Entities;

public class UserDetail
{
    public long UserDetailsId { get; set; }
    public string FullName { get; set; } = null!;
    public string InstituteName { get; set; } = null!;
    public string IdCardNumber { get; set; } = null!;
    
    public long? UserId { get; set; }
    public User User { get; set; } = null!;
}