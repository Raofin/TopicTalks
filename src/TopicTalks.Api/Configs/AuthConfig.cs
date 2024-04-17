using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TopicTalks.Application.Common;
using TopicTalks.Domain.Enums;

namespace TopicTalks.Api.Configs;

public static class AuthConfig
{
    public static void AddAuthConfig(this WebApplicationBuilder builder)
    {
        var jwtSettings = new JwtSettings();
        builder.Configuration.Bind(nameof(JwtSettings), jwtSettings);

        builder.Services.AddAuthorization(options => {
            options.AddPolicy(nameof(RoleType.Student), policy => policy.RequireRole(nameof(RoleType.Student)));
            options.AddPolicy(nameof(RoleType.Teacher), policy => policy.RequireRole(nameof(RoleType.Teacher)));
            options.AddPolicy(nameof(RoleType.Moderator), policy => policy.RequireRole(nameof(RoleType.Moderator)));
        });

        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
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