using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using TopicTalks.Application.Common;
using TopicTalks.Infrastructure.Persistence;

namespace TopicTalks.Api.Configs;

public static class DatabaseConfig
{
    public static IApplicationBuilder ApplyMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var databaseCreator = dbContext.GetService<IRelationalDatabaseCreator>();

        if (!databaseCreator.Exists())
        {
            dbContext.Database.Migrate();
        }

        return app;
    }

    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        var connectionStrings = new ConnectionStrings();
        builder.Configuration.Bind(nameof(ConnectionStrings), connectionStrings);

        builder.Services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(connectionStrings.DefaultConnection));
    }
}