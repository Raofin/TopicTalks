﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicTalks.Domain.Entities;

namespace TopicTalks.Infrastructure.Persistence.Configurations;

public class CloudFileConfigurations : IEntityTypeConfiguration<CloudFile>
{
    public void Configure(EntityTypeBuilder<CloudFile> entity)
    {
        // Table and Key Configuration
        entity.ToTable("CloudFiles", "core");
        entity.HasKey(c => c.CloudFileId);

        // Configure the properties
        entity.Property(c => c.CloudFileId).IsRequired().HasMaxLength(255).ValueGeneratedNever();
        entity.Property(c => c.Name).IsRequired().HasMaxLength(255);
        entity.Property(c => c.ContentType).IsRequired().HasMaxLength(255);
        entity.Property(c => c.Size).IsRequired().HasComment("Bytes");
        entity.Property(c => c.WebContentLink).IsRequired().HasMaxLength(255);
        entity.Property(c => c.WebViewLink).IsRequired().HasMaxLength(255);
        entity.Property(c => c.DirectLink).IsRequired().HasMaxLength(255);
        entity.Property(c => c.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("getUtcDate()");
        entity.Property(c => c.UserId).IsRequired(false);

        // Relationship Configuration
        entity.HasOne(c => c.User)
            .WithMany(u => u.CloudFiles)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}