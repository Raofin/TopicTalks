using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicTalks.Domain.Entities;

namespace TopicTalks.Infrastructure.Persistence.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        // Table and Key Configuration
        entity.ToTable("Users", "auth");
        entity.HasKey(e => e.UserId);

        // Property Configuration
        entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
        entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
        entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
        entity.Property(e => e.Salt).IsRequired().HasMaxLength(255);
        entity.Property(e => e.IsVerified).HasDefaultValue(false);
        entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("getUtcDate()");
        entity.Property(e => e.ImageFileId).IsRequired(false).HasMaxLength(255);

        // Relationship Configuration
        entity.HasOne(d => d.ImageFile)
            .WithMany(p => p.Users)
            .HasForeignKey(d => d.ImageFileId)
            .OnDelete(DeleteBehavior.SetNull);

        // Index Configurations
        entity.HasIndex(e => e.Email).IsUnique().HasDatabaseName("IX_User_Email");
        entity.HasIndex(e => e.Username).IsUnique().HasDatabaseName("IX_User_Username");
    }
}