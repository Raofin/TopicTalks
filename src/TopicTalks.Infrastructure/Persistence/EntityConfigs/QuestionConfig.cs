using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicTalks.Domain.Entities;

namespace TopicTalks.Infrastructure.Persistence.EntityConfigs;

public class QuestionConfig : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> entity)
    {
        // Table Configuration
        entity.ToTable("Questions", "post");
        entity.HasKey(e => e.QuestionId);

        // Property Configuration
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.Explanation)
            .IsRequired();

        entity.Property(e => e.Topic)
            .IsRequired()
            .HasMaxLength(50);

        entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

        // Relationship Configuration
        entity.HasOne(d => d.User)
            .WithMany(p => p.Questions)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_Questions_Users");

        // Index Configuration
        entity.HasIndex(e => e.UserId, "IX_Questions_UserId");
    }
}