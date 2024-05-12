using TopicTalks.Application;
using TopicTalks.Infrastructure;
using TopicTalks.Api;
using TopicTalks.Razor;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddRazor(builder.Environment);

builder.Host.UseSerilogSqlServer(builder.Configuration, builder.Environment);

var app = builder.Build();

app.UseInfrastructure(builder.Environment);

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers().RequireAuthorization();
app.MapHealthChecks("health");

app.Run();