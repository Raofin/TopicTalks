using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TopicTalks.Domain.Enums;
using TopicTalks.Api.Common;

namespace TopicTalks.Api.Configs;

public static class AuthConfig
{
    public static void AddAuthConfig(this IServiceCollection services)
    {
        services.AddAuthorization(options => {
            options.AddPolicy(RoleType.Student.ToString(), policy => policy.RequireRole(RoleType.Student.ToString()));
            options.AddPolicy(RoleType.Teacher.ToString(), policy => policy.RequireRole(RoleType.Teacher.ToString()));
            options.AddPolicy(RoleType.Moderator.ToString(),
                policy => policy.RequireRole(RoleType.Moderator.ToString()));
        });

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SettingsFetcher.JwtKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "https://rawfin.net",
                    ValidAudience = "https://rawfin.net",
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
    }
}