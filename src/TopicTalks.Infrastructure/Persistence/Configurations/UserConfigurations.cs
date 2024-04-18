using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicTalks.Domain.Entities;

namespace TopicTalks.Infrastructure.Persistence.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        // Table Configuration
        entity.ToTable("Users", "auth");

        // Property Configuration
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(255);

        entity.Property(e => e.PasswordHash)
            .IsRequired();

        entity.Property(e => e.Salt)
            .IsRequired();

        entity.Property(e => e.IsVerified)
            .HasDefaultValue(false);
    }
}