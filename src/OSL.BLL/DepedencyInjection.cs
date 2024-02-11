using Microsoft.Extensions.DependencyInjection;
using OSL.BLL.Interfaces;
using OSL.BLL.Services;

namespace OSL.BLL;

public static class DepedencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPasswordHashService, PasswordHashService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IAnswerService, AnswerService>();

        return services;
    }
}
