using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicTalks.Domain.Entities;

namespace TopicTalks.Infrastructure.Persistence.Configurations;

public class UserDetailConfigurations : IEntityTypeConfiguration<UserDetail>
{
    public void Configure(EntityTypeBuilder<UserDetail> entity)
    {
        // Table and Key Configuration
        entity.ToTable("UserDetails", "auth");
        entity.HasKey(e => e.UserDetailsId);

        // Property Configuration
        entity.Property(e => e.FullName).IsRequired().HasMaxLength(255);
        entity.Property(e => e.InstituteName).IsRequired().HasMaxLength(255);
        entity.Property(e => e.IdCardNumber).IsRequired().HasMaxLength(50);

        // Relationship Configuration
        entity.HasOne(d => d.User)
            .WithOne(p => p.UserDetails)
            .HasForeignKey<UserDetail>(d => d.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}