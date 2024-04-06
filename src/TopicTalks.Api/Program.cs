using FluentValidation;
using TopicTalks.Application;
using FluentValidation.AspNetCore;
using TopicTalks.Api.Configs;
using TopicTalks.Infrastructure;
using TopicTalks.Api.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();
builder.Services.AddHealthChecks();

builder.Services.AddAuthConfig();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.AddDatabase();
builder.AddCorsConfig();
builder.AddSettingFetcher();

DinkToPdfAllOs.LibraryLoader.Load();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.ApplyMigration();
app.UseCustomCors();
app.UseHttpsRedirection();
app.MapControllers();
app.MapHealthChecks("health");

app.UseAuthentication();
app.UseAuthorization();

app.Run();