namespace TopicTalks.Api.Configs;

public static class EmailConfig
{
    public static void AddEmailConfig(this WebApplicationBuilder builder)
    {
        var setting = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>()!;

        builder.Services.AddSingleton(setting);
        builder.Services
            .AddFluentEmail(setting.Email, setting.Name)
            .AddSmtpSender(setting.Server, setting.Port, setting.Email, setting.Password.Replace(" ", string.Empty));
    }
}

internal record EmailSettings(
    string Server,
    int Port,
    string Name,
    string Email,
    string Password
);