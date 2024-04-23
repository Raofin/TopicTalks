using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicTalks.Domain.Entities;

namespace TopicTalks.Infrastructure.Persistence.Configurations;

public class RoleConfigurations : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> entity)
    {
        // Table and Key Configuration
        entity.ToTable("Roles", "enum");
        entity.HasKey(e => e.RoleId);

        // Property Configuration
        entity.Property(e => e.RoleName).IsRequired().HasMaxLength(50);
    }
}