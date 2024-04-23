using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicTalks.Domain.Entities;

namespace TopicTalks.Infrastructure.Persistence.Configurations;

public class AnswerConfigurations : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> entity)
    {
        // Table and Key Configuration
        entity.ToTable("Answers", "post");
        entity.HasKey(x => x.AnswerId);

        // Property Configuration
        entity.Property(e => e.ParentAnswerId).HasDefaultValue(0L);
        entity.Property(e => e.Explanation).IsRequired();
        entity.Property(e => e.IsNotified).HasDefaultValue(true);
        entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("getUtcDate()");

        // Relationship Configuration
        entity.HasOne(d => d.User)
            .WithMany(p => p.Answers)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_Answers_Users");
    }
}