using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicTalks.Domain.Entities;

namespace TopicTalks.Infrastructure.Persistence.Configurations;

public class CloudFileConfigurations : IEntityTypeConfiguration<Cloud>
{
    public void Configure(EntityTypeBuilder<Cloud> entity)
    {
        // Table and Key Configuration
        entity.ToTable("CloudFiles", "core");
        entity.HasKey(c => c.CloudFileId);

        // Configure the properties
        entity.Property(c => c.CloudFileId).IsRequired().HasMaxLength(255).ValueGeneratedNever();
        entity.Property(c => c.Name).IsRequired().HasMaxLength(255);
        entity.Property(c => c.ContentType).IsRequired().HasMaxLength(255);
        entity.Property(c => c.Size).IsRequired();
        entity.Property(c => c.WebContentLink).IsRequired().HasMaxLength(255);
        entity.Property(c => c.WebViewLink).IsRequired().HasMaxLength(255);
        entity.Property(c => c.DirectLink).IsRequired().HasMaxLength(255);
        entity.Property(c => c.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("(getutcdate())");
        entity.Property(c => c.UserId).IsRequired(false);

        // Relationship Configuration
        entity.HasOne(c => c.User)
            .WithMany(u => u.CloudFiles)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CloudFiles_Users");

        // Index Configuration
        entity.HasIndex(c => c.UserId, "IX_CloudFiles_UserId");
    }
}