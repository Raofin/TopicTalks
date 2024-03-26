using Microsoft.AspNetCore.Authentication.Cookies;
using TopicTalks.Web.Enums;

namespace TopicTalks.Web.Configs;

public static class AuthConfig
{
    public static void AddAuthConfig(this IServiceCollection services)
    {
        // Configure authorization policies for different roles
        services.AddAuthorization(options => {
            options.AddPolicy(RoleType.Student.ToString(), policy => policy.RequireRole(RoleType.Student.ToString()));
            options.AddPolicy(RoleType.Teacher.ToString(), policy => policy.RequireRole(RoleType.Teacher.ToString()));
            options.AddPolicy(RoleType.Moderator.ToString(), policy => policy.RequireRole(RoleType.Moderator.ToString()));
        });

        // Configure cookie authentication
        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => {
                options.LoginPath = "/Login";
                options.LogoutPath = "/logout";
                options.AccessDeniedPath = "/401";
                options.ExpireTimeSpan = TimeSpan.FromDays(7);

                // CSRF attack prevention
                options.Cookie.Name = "TTs.Cookies";
                options.Cookie.HttpOnly = true; // Make cookie accessible only via HTTP
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Require HTTPS for cookie
                options.Cookie.SameSite = SameSiteMode.Strict; // Apply strict SameSite policy
            });
    }
}