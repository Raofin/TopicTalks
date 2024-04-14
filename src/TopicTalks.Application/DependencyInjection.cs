using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TopicTalks.Application.Interfaces;
using TopicTalks.Application.Services;
using TopicTalks.Application.Services.Pdf;
using TopicTalks.Application.Interfaces.Pdf;
using TopicTalks.Application.Interfaces.Excel;
using TopicTalks.Application.Services.Excel;

namespace TopicTalks.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasher<IdentityUser>, PasswordHasher<IdentityUser>>();
        services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
        services.AddSingleton<IExcelExportService, ExcelExportService>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IAnswerService, AnswerService>();
        services.AddScoped<IEmailService, EmailService>();

        services.AddTransient<IPdfGeneratorService, PdfGeneratorService>();
        services.AddTransient<IPdfService, PdfService>();

        return services;
    }
}
