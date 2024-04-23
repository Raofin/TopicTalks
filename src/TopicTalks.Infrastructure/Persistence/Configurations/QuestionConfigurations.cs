using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicTalks.Domain.Entities;

namespace TopicTalks.Infrastructure.Persistence.Configurations;

public class QuestionConfigurations : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> entity)
    {
        // Table and Key Configuration
        entity.ToTable("Questions", "post");
        entity.HasKey(e => e.QuestionId);

        // Property Configuration
        entity.Property(e => e.Topic).IsRequired().HasMaxLength(50);
        entity.Property(e => e.Explanation).IsRequired();
        entity.Property(e => e.IsNotified).HasDefaultValue(true);
        entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("getUtcDate()");
        entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        entity.Property(e => e.ImageFileId).IsRequired(false).HasMaxLength(255);

        // Relationship Configuration
        entity.HasOne(d => d.ImageFile)
            .WithMany(p => p.Questions)
            .HasForeignKey(d => d.ImageFileId)
            .OnDelete(DeleteBehavior.SetNull);

        entity.HasOne(d => d.User)
            .WithMany(p => p.Questions)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}