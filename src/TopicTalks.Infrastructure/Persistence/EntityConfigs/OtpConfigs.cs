using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TopicTalks.Domain.Entities;

namespace TopicTalks.Infrastructure.Persistence.EntityConfigs;

public class OtpConfig : IEntityTypeConfiguration<Otp>
{
    public void Configure(EntityTypeBuilder<Otp> entity)
    {
        // Table Configuration
        entity.ToTable("Otps", "auth");
        entity.HasKey(e => e.Email);

        // Property Configuration
        entity.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(255);

        entity.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(6);

        entity.Property(o => o.ExpiresAt)
            .HasDefaultValueSql("DATEADD(MINUTE, 5, GETDATE())");
    }
}