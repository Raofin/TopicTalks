using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TopicTalks.Domain.Entities;

namespace TopicTalks.Infrastructure.Persistence.Configurations;

public class LogEventConfigurations : IEntityTypeConfiguration<LogEvent>
{
    public void Configure(EntityTypeBuilder<LogEvent> entity)
    {
        // Table and Key Configuration
        entity.ToTable("LogEvents");
        entity.HasKey(e => e.Id);

        // Property Configuration
        entity.Property(e => e.Message).HasColumnType("nvarchar(max)").IsRequired(false);
        entity.Property(e => e.MessageTemplate).HasColumnType("nvarchar(max)").IsRequired(false);
        entity.Property(e => e.Level).HasColumnType("nvarchar(max)").IsRequired(false);
        entity.Property(e => e.TimeStamp).HasColumnType("datetime");
        entity.Property(e => e.Exception).HasColumnType("nvarchar(max)").IsRequired(false);
        entity.Property(e => e.Properties).HasColumnType("nvarchar(max)").IsRequired(false);
    }
}