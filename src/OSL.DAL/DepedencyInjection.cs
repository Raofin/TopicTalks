using Microsoft.Extensions.DependencyInjection;
using OSL.DAL.Interfaces;
using OSL.DAL.Repositories;

namespace OSL.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();

        return services;
    }
}