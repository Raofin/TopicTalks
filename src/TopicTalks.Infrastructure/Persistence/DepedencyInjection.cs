using Microsoft.Extensions.DependencyInjection;
using TopicTalks.Domain.Interfaces;
using TopicTalks.Infrastructure.Persistence.Repositories;

namespace TopicTalks.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IAnswerRepository, AnswerRepository>();

        return services;
    }
}