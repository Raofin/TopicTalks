namespace TopicTalks.Web.Common;

public class AppSettings
{
    public string ApiBaseUrl { get; set; } = null!;
    public JwtSettings JwtSettings { get; set; } = null!;
}

public class JwtSettings
{
    public string Secret { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
}
