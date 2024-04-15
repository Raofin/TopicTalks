namespace TopicTalks.Domain.Entities;

public class Otp
{
    public string Email { get; set; } = null!;
    public string Code { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
}
