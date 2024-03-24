#pragma warning disable CS8618

namespace TopicTalks.Web.Configs;

internal static class AppSettings
{
    private static IConfiguration _configuration;

    public static void InitializeAppSettings(this WebApplicationBuilder builder)
    {
        _configuration = builder.Configuration;
    }

    public static string ApiBaseUrl => _configuration.GetValue<string>("ApiBaseUrl") ?? throw new InvalidOperationException("Api URL has not been initialized.");
}