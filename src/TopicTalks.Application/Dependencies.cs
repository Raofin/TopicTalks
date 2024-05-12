using Microsoft.Extensions.DependencyInjection;
using TopicTalks.Application.BackgroundServices;
using TopicTalks.Application.Interfaces;
using TopicTalks.Application.Services;

namespace TopicTalks.Application;

public static class Dependencies
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IAnswerService, AnswerService>();
        services.AddScoped<IExcelService, ExcelService>();
        services.AddScoped<ICloudService, CloudService>();

        services.AddHostedService<OtpCleanupService>();

        return services;
    }
}
