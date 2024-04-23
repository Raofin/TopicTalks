using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicTalks.Domain.Entities;

namespace TopicTalks.Infrastructure.Persistence.Configurations;

public class OtpConfig : IEntityTypeConfiguration<Otp>
{
    public void Configure(EntityTypeBuilder<Otp> entity)
    {
        // Table and Key Configuration
        entity.ToTable("Otps", "auth");
        entity.HasKey(e => e.Email);

        // Property Configuration
        entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
        entity.Property(e => e.Code).IsRequired().HasMaxLength(6);
        entity.Property(o => o.ExpiresAt).HasColumnType("datetime").HasDefaultValueSql("dateAdd(minute, 5, getUtcDate())");
    }
}