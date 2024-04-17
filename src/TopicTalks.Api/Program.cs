using FluentValidation;
using TopicTalks.Application;
using FluentValidation.AspNetCore;
using TopicTalks.Api.Configs;
using TopicTalks.Infrastructure;
using TopicTalks.Api;
using TopicTalks.Application.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();
builder.Services.AddHealthChecks();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();
builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.AddDatabase();
builder.AddAuthConfig();
builder.AddCorsConfig();
builder.AddEmailConfig();

DinkToPdfAllOs.LibraryLoader.Load();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.ApplyMigration();
app.UseCustomCors();
app.UseHttpsRedirection();
app.UseHostFiltering();

app.MapControllers();
app.MapHealthChecks("health");

app.UseAuthentication();
app.UseAuthorization();

app.Run();