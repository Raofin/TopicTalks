﻿using Microsoft.EntityFrameworkCore;
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

        modelBuilder.Entity<User>().HasData(
            new User { UserId = 1, Email = "hello@rawfin.net", PasswordHash = "AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==", Salt = "vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=", CreatedAt = DateTime.Now },
            new User { UserId = 2, Email = "doe@email.net", PasswordHash = "AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==", Salt = "vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=", CreatedAt = DateTime.Now },
            new User { UserId = 3, Email = "bob@email.net", PasswordHash = "AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==", Salt = "vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=", CreatedAt = DateTime.Now }
        );

        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { UserRoleId = 1, UserId = 1, RoleId = 1 },
            new UserRole { UserRoleId = 2, UserId = 2, RoleId = 2 },
            new UserRole { UserRoleId = 3, UserId = 3, RoleId = 3 }
        );

        modelBuilder.Entity<UserDetail>().HasData(
            new UserDetail { UserDetailsId = 1, UserId = 1, Name = "Rawfin", InstituteName = "AIUB", IdCardNumber = "20-42459-1" }
        );

        modelBuilder.Entity<Question>().HasData(
            new Question { QuestionId = 1, Topic = "C# 12, Code Syntax, Maintainability", Explanation = "In C# 12, what are the advantages and trade-offs of using record types with pattern matching and deconstruction in ASP.NET 8 code, considering maintainability, readability, and potential performance implications?", UserId = 1, CreatedAt = DateTime.Now },
            new Question { QuestionId = 2, Topic = "ASP.NET 8, HTTP Caching, Request Caching", Explanation = "With ASP.NET 8's improved request caching and HTTP caching strategies, in what scenarios could you effectively combine them to achieve optimal performance gains across different data access patterns (in-memory, database, external APIs)?", UserId = 1, CreatedAt = DateTime.Now },
            new Question { QuestionId = 3, Topic = "ASP.NET 8, Development Workflow, Live Updates", Explanation = "What are the use cases for ASP.NET 8's hot reload capability, and how can it improve development workflow and reduce downtime in production environments?", UserId = 1, CreatedAt = DateTime.Now },
            new Question { QuestionId = 4, Topic = "C#, Java, Developer Experience", Explanation = "As a C# developer comfortable with Microsoft ecosystem, is Spring Boot worth exploring even though it uses Java? When might switching make sense, if ever?", UserId = 1, CreatedAt = DateTime.Now },
            new Question { QuestionId = 5, Topic = "ASP.NET, Spring Boot, Data Persistence", Explanation = "When working with diverse data sources and integration needs, how do ASP.NET Core's Entity Framework Core and Spring Boot's Spring Data JPA compare in terms of ease of use, performance, and integration capabilities?", UserId = 1, CreatedAt = DateTime.Now },
            new Question { QuestionId = 6, Topic = "React Ecosystem, Mobile Apps, Desktop Apps", Explanation = "How can you adapt React development for building mobile apps with React Native, desktop applications with Electron, or server-side rendering with Next.js?", UserId = 1, CreatedAt = DateTime.Now }
        );

        modelBuilder.Entity<Answer>().HasData(
            new Answer { AnswerId = 1, ParentAnswerId = 0, QuestionId = 1, Explanation = "ASP.NET 8, with the introduction of C# 12's record types, pattern matching, and deconstruction, presents a nuanced landscape for developers. The advantages are evident, as records facilitate encapsulated immutability, promoting cleaner code and reducing potential errors. Deconstructing records enhances readability, while pattern matching introduces expressive conditionals. Performance gains are plausible, with optimized equality checks and accelerated property access. However, the learning curve associated with these features and the potential inflexibility for complex data modifications pose challenges. Moreover, records lack direct inheritance support, requiring adjustments to established design patterns. In conclusion, while records and pattern matching offer significant advantages in terms of immutability and readability, a thoughtful evaluation of trade-offs is imperative. The decision to adopt these features should align with the project's goals, team's expertise, and the potential learning curve, ensuring a judicious integration without compromising project success.", UserId = 2, CreatedAt = DateTime.Now },
            new Answer { AnswerId = 2, ParentAnswerId = 1, QuestionId = 1, Explanation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed, hendrerit?", UserId = 1, CreatedAt = DateTime.Now },
            new Answer { AnswerId = 3, ParentAnswerId = 2, QuestionId = 1, Explanation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et.", UserId = 1, CreatedAt = DateTime.Now },
            new Answer { AnswerId = 4, ParentAnswerId = 0, QuestionId = 1, Explanation = "Lorem ipsum dolor sit amet, consectetur adipiscing.", UserId = 2, CreatedAt = DateTime.Now },
            new Answer { AnswerId = 5, ParentAnswerId = 4, QuestionId = 1, Explanation = "Lorem ipsum dolor sit amet.", UserId = 1, CreatedAt = DateTime.Now }
        );

        base.OnModelCreating(modelBuilder);
    }
}