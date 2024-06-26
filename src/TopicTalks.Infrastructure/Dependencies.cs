﻿using Microsoft.Extensions.DependencyInjection;
using TopicTalks.Domain;
using TopicTalks.Infrastructure.Persistence;
using TopicTalks.Infrastructure.Persistence.Repositories;
using TopicTalks.Infrastructure.Services;
using TopicTalks.Domain.Interfaces.Repositories;
using TopicTalks.Domain.Interfaces.Core;
using TopicTalks.Infrastructure.Services.Cloud;
using TopicTalks.Infrastructure.Services.Email;
using TopicTalks.Infrastructure.Services.Pdf;
using TopicTalks.Infrastructure.Services.Token;

namespace TopicTalks.Infrastructure;

internal static class Dependencies
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IHashPassword, HashPassword>();
        services.AddSingleton<IJwtGenerator, JwtGenerator>();

        services.AddScoped<IExcelGenerator, ExcelGenerator>();
        services.AddScoped<IPdfGenerator, PdfGenerator>();
        services.AddScoped<IGoogleCloud, GoogleCloud>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IWwwootService, WwwootService>();
        services.AddScoped<IUserInfoProvider, UserInfoProvider>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IAnswerRepository, AnswerRepository>();
        services.AddScoped<IOtpRepository, OtpRepository>();
        services.AddScoped<ICloudRepository, CloudRepository>();

        return services;
    }
}