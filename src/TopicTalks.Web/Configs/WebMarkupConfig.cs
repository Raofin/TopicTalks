using WebMarkupMin.AspNetCore8;

namespace TopicTalks.Web.Configs;

public static class WebMarkupMinConfig
{
    public static void InitializeWebMarkupMin(this IServiceCollection services)
    {
        services
            .AddWebMarkupMin(
                options => {
                    options.AllowMinificationInDevelopmentEnvironment = false;
                    options.AllowCompressionInDevelopmentEnvironment = false;
                })
            .AddHtmlMinification(
                options => {
                    options.MinificationSettings.RemoveRedundantAttributes = true;
                    options.MinificationSettings.RemoveHttpProtocolFromAttributes = true;
                    options.MinificationSettings.RemoveHttpsProtocolFromAttributes = true;
                })
            .AddHttpCompression();

    }
}