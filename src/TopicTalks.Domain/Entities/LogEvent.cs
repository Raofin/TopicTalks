namespace TopicTalks.Domain.Entities;

public class LogEvent
{
    public int Id { get; set; }
    public string Message { get; set; } = null!;
    public string MessageTemplate { get; set; } = null!;
    public string Level { get; set; } = null!;
    public DateTime TimeStamp { get; set; }
    public string Exception { get; set; } = null!;
    public string Properties { get; set; } = null!;
}
