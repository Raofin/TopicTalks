namespace TopicTalks.Domain.Entities;

public class User
{
    public long UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Salt { get; set; } = null!;
    public bool IsVerified { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    
    public string? ImageFileId { get; set; }
    public CloudFile? ImageFile { get; set; }

    public UserDetail? UserDetails { get; set; }
    public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    public ICollection<Question> Questions { get; set; } = new List<Question>();
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<CloudFile> CloudFiles { get; set; } = new List<CloudFile>();
}