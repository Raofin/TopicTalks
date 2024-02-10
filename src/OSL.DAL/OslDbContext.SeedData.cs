using Microsoft.EntityFrameworkCore;
using OSL.DAL.Entities;

namespace OSL.DAL;

public partial class OslDbContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { RoleId = 1, RoleName = "Student" },
            new Role { RoleId = 2, RoleName = "Teacher" },
            new Role { RoleId = 3, RoleName = "Moderator" }
        );

        base.OnModelCreating(modelBuilder);
    }
}