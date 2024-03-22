using Microsoft.EntityFrameworkCore;
using TopicTalks.Domain.Entities;

namespace TopicTalks.Infrastructure.Persistence;

public partial class TopicTalksDbContext : DbContext
{
    public TopicTalksDbContext()
    {
    }

    public TopicTalksDbContext(DbContextOptions<TopicTalksDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.ToTable("Answers", "post");

            entity.HasIndex(e => e.ParentAnswerId, "IX_Answers_ParentAnswerId");

            entity.HasIndex(e => e.QuestionId, "IX_Answers_QuestionId");

            entity.HasIndex(e => e.UserId, "IX_Answers_UserId");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ParentAnswerId).HasDefaultValue(0L);

            entity.HasOne(d => d.User).WithMany(p => p.Answers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Answers_Users");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.ToTable("Questions", "post");

            entity.HasIndex(e => e.UserId, "IX_Questions_UserId");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Topic).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Questions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Questions_Users");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Roles", "enum");

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users", "auth");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UserDetailsId);

            entity.ToTable("UserDetails", "auth");

            entity.HasIndex(e => e.UserId, "IX_UserDetails_UserId");

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

            entity.HasIndex(e => e.RoleId, "IX_UserRoles_RoleId");

            entity.HasIndex(e => e.UserId, "IX_UserRoles_UserId");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_UserRoles_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_UserRoles_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
