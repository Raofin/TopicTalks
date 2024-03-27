using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TopicTalks.Domain.Enums;

namespace TopicTalks.Api.Configs;

public static class AuthConfig
{
    public static void AddAuthConfig(this IServiceCollection services)
    {
        // Configure authorization policies for different roles
        services.AddAuthorization(options => {
            options.AddPolicy(RoleType.Student.ToString(), policy => policy.RequireRole(RoleType.Student.ToString()));
            options.AddPolicy(RoleType.Teacher.ToString(), policy => policy.RequireRole(RoleType.Teacher.ToString()));
            options.AddPolicy(RoleType.Moderator.ToString(),
                policy => policy.RequireRole(RoleType.Moderator.ToString()));
        });

        // Configure cookie authentication
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true, // Validate the security key when validating the token
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppSettingsFetcher.JwtKey)), // Set the security key
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "https://rawfin.net",
                    ValidAudience = "https://rawfin.net",
                    RequireExpirationTime = true, // Require to have an expiration time
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
    }
}