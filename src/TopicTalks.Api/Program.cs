using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TopicTalks.Infrastructure.Persistence;
using TopicTalks.Application;
using FluentValidation.AspNetCore;
using TopicTalks.Api.Configs;
using TopicTalks.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppSettingFetcher();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();
builder.Services.AddHealthChecks();
builder.Services.AddHttpContextAccessor();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthConfig();
builder.Services.AddCors(options => {
    options.AddPolicy("AllowOrigin",
        policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

DinkToPdfAll.LibraryLoader.Load();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("health");

app.UseCors("AllowOrigin");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();