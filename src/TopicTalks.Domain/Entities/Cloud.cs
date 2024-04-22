namespace TopicTalks.Domain.Entities;

public class Cloud
{
    public string CloudFileId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public long Size { get; set; }
    public string WebContentLink { get; set; } = null!;
    public string WebViewLink { get; set; } = null!;
    public string DirectLink { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public long? UserId { get; set; }

    public User User { get; set; } = null!;
}