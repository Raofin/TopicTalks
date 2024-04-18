using Microsoft.Extensions.DependencyInjection;
using TopicTalks.Domain;
using TopicTalks.Domain.Interfaces;
using TopicTalks.Infrastructure.Persistence;
using TopicTalks.Infrastructure.Persistence.Repositories;

namespace TopicTalks.Infrastructure;

internal static class Dependencies
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IAnswerRepository, AnswerRepository>();
        services.AddScoped<IOtpRepository, OtpRepository>();

        return services;
    }
}