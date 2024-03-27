#pragma warning disable CS8618
#pragma warning disable CS8603

namespace TopicTalks.Api.Configs;

internal static class AppSettingsFetcher
{
    private static IConfiguration _configuration;

    public static void AddAppSettingFetcher(this WebApplicationBuilder builder)
    {
        _configuration = builder.Configuration;
    }

    public static string JwtKey => _configuration.GetValue<string>("Jwt:Key");
}