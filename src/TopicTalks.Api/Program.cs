using TopicTalks.Application;
using TopicTalks.Infrastructure;
using TopicTalks.Api;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration, builder.Environment);

    builder.Host.AddSerilog(builder.Configuration, builder.Environment);
}

var app = builder.Build();
{
    app.UseInfrastructure(app.Environment);
    app.MapControllers().RequireAuthorization();
    app.MapHealthChecks("health");
    
    if (true /*env.IsDevelopment()*/)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    app.Run();
}