namespace TopicTalks.Application.Common;

public record AppSettings
{
    public ConnectionStrings ConnectionStrings { get; init; } = null!;
    public JwtSettings JwtSettings { get; init; } = null!;
    public EmailSettings EmailSettings { get; init; } = null!;
}

public record ConnectionStrings
{
    public string DefaultConnection { get; set; } = null!;
}

public record JwtSettings
{
    public string Secret { get; init; } = null!;
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
}

public record EmailSettings
{
    private readonly string _password = null!;

    public string Server { get; init; } = null!;
    public int Port { get; init; }
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;

    public string Password {
        get => _password;
        init => _password = value.Replace(" ", "");
    }
}
