using Microsoft.Extensions.DependencyInjection;
using TopicTalks.Domain;
using TopicTalks.Domain.Interfaces;
using TopicTalks.Infrastructure.Persistence;
using TopicTalks.Infrastructure.Persistence.Repositories;

namespace TopicTalks.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IAnswerRepository, AnswerRepository>();
        services.AddScoped<IOtpRepository, OtpRepository>();

        return services;
    }
}