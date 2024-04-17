using TopicTalks.Application.Common;

namespace TopicTalks.Api.Configs;

public static class EmailConfig
{
    public static void AddEmailConfig(this WebApplicationBuilder builder)
    {
        var settings = new EmailSettings();
        builder.Configuration.Bind(nameof(EmailSettings), settings);

        builder.Services
            .AddFluentEmail(settings.Email, settings.Name)
            .AddSmtpSender(settings.Server, settings.Port, settings.Email, settings.Password);
    }
}