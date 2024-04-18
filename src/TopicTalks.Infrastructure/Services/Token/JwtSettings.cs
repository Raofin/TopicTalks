namespace TopicTalks.Infrastructure.Services.Token;

public class JwtSettings
{
    public string Secret { get; set; } = null!;
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
}