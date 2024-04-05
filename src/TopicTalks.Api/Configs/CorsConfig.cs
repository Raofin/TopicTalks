namespace TopicTalks.Api.Configs;

public static class CorsConfig
{
    public static void AddCorsConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options => {
            options.AddPolicy("AllowSpecificOrigins",
                policyBuilder => {
                    policyBuilder.WithOrigins("https://*.rawfin.net")
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });

            options.AddPolicy("AllowAnyOrigin",
                policyBuilder => {
                    policyBuilder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
    }

    public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
    {
        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
        var policyName = env.IsDevelopment() ? "AllowAnyOrigin" : "AllowSpecificOrigins";

        app.UseCors(policyName);
        return app;
    }
}