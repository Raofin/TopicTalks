using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TopicTalks.Application.Interfaces;
using TopicTalks.Application.Services;

namespace TopicTalks.Application;

public static class DepedencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasher<IdentityUser>, PasswordHasher<IdentityUser>>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IAnswerService, AnswerService>();

        return services;
    }
}
