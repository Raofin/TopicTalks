using Microsoft.AspNetCore.Authentication.Cookies;
using NUglify.Css;
using NUglify.JavaScript;
using Serilog;
using Serilog.Events;
using TopicTalks.Web.Common;
using WebMarkupMin.AspNetCore8;

namespace TopicTalks.Web;

public static class AppConfigurations
{

    public static IServiceCollection AddAppConfigurations(this IServiceCollection services, IWebHostEnvironment environment)
    {
        services
            .AddAuthorization()
            .AddAuthentication()
            .AddWebMarkupMin()
            .AddWebOptimizer(environment)
            .AddCorsConfiguration();

        return services;
    }

    public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
    {
        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
        var policyName = env.IsDevelopment() ? "AllowAnyOrigin" : "AllowSpecificOrigins";

        app.UseCors(policyName);
        return app;
    }

    public static IServiceCollection AddHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        var api = new Api();
        configuration.Bind(nameof(api), api);

        services.AddHttpClient("TT_Api", c => c.BaseAddress = new Uri(api.BaseUrl));
        return services;
    }

    #region ### CORS ###

    private static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
    {
        services.AddCors(options => {
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

        return services;
    }

    #endregion

    #region ### Authentication & Authorization ###

    private static IServiceCollection AddAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options => {
            options.AddPolicy(RoleType.Student.ToString(), policy => policy.RequireRole(RoleType.Student.ToString()));
            options.AddPolicy(RoleType.Teacher.ToString(), policy => policy.RequireRole(RoleType.Teacher.ToString()));
            options.AddPolicy(RoleType.Moderator.ToString(), policy => policy.RequireRole(RoleType.Moderator.ToString()));
        });

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services)
    {
        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => {
                options.LoginPath = "/Login";
                options.LogoutPath = "/logout";
                options.AccessDeniedPath = "/401";
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.Cookie.Name = "TTs.Cookies";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
            });

        return services;
    }

    #endregion

    #region ### WebMarkupMin & WebOptimizer ###

    private static IServiceCollection AddWebMarkupMin(this IServiceCollection services)
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

        return services;
    }

    private static IServiceCollection AddWebOptimizer(this IServiceCollection services, IHostEnvironment environment)
    {
        if (!environment.IsDevelopment())
        {
            services.AddWebOptimizer(pipeline => {
                pipeline.AddCssBundle("/css/bundle.css", new CssSettings { MinifyExpressions = false }, "css/*.css");
                pipeline.AddJavaScriptBundle("/js/bundle.js", new CodeSettings { MinifyCode = false }, "js/*.js");
            }, option => {
                option.EnableCaching = false;
                option.EnableDiskCache = false;
                option.EnableMemoryCache = false;
            });
        }
        else
        {
            services.AddWebOptimizer(pipeline => {
                pipeline.AddCssBundle("/css/bundle.css", "css/*.css");
                pipeline.AddJavaScriptBundle("/js/bundle.js", "js/*.js");
            });

        }

        return services;
    }

    #endregion

    #region ### Serilog ###

    public static IHostBuilder UseSerilogConfig(this IHostBuilder hostBuilder, IHostEnvironment env)
    {
        hostBuilder.UseSerilog((context, config) =>
            config.ReadFrom.Configuration(context.Configuration)
                .WriteTo.Console(env.IsDevelopment() ? LogEventLevel.Information : LogEventLevel.Error)
        );

        return hostBuilder;
    }

    #endregion
}