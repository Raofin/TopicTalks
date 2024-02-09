﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OSL.DAL.Entities;

namespace OSL.DAL;

public partial class OSLContext : DbContext
{
    public OSLContext()
    {
    }

    public OSLContext(DbContextOptions<OSLContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Roles", "enum");

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users", "auth");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UserDetailsId);

            entity.ToTable("UserDetails", "auth");

            entity.Property(e => e.IdCardNumber).HasMaxLength(50);
            entity.Property(e => e.InstituteName).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_UserDetails_Users");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRoles", "auth");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}