#pragma warning disable CS8618 // Non-nullable field is uninitialized
#pragma warning disable CS8603 // Possible null reference return

namespace TopicTalks.Api.Common;

internal static class SettingsFetcher
{
    private static IConfiguration _configuration;

    public static void AddSettingFetcher(this WebApplicationBuilder builder)
    {
        _configuration = builder.Configuration;
    }

    public static string JwtKey => _configuration.GetValue<string>("Jwt:Key");
}