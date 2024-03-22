using Microsoft.Extensions.DependencyInjection;
using TopicTalks.Application.Interfaces;
using TopicTalks.Application.Services;
using TopicTalks.Domain.Interfaces;

namespace TopicTalks.Application;

public static class DepedencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPasswordHashService, PasswordHashService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IAnswerService, AnswerService>();

        return services;
    }
}
