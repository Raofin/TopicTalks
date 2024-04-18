using TopicTalks.Application;
using TopicTalks.Infrastructure;
using TopicTalks.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);


var app = builder.Build();

app.UseInfrastructure();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers().RequireAuthorization();
app.MapHealthChecks("health");

app.Run();