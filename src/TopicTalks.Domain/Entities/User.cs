namespace TopicTalks.Domain.Entities;

public class User
{
    public long UserId { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public ICollection<Question> Questions { get; set; } = new List<Question>();

    public UserDetail? UserDetails { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}