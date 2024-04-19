using Microsoft.EntityFrameworkCore;
using TopicTalks.Domain.Entities;

namespace TopicTalks.Infrastructure.Persistence;

public static class SeedData
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { RoleId = 1, RoleName = "Student" },
            new Role { RoleId = 2, RoleName = "Teacher" },
            new Role { RoleId = 3, RoleName = "Moderator" }
        );

        modelBuilder.Entity<User>().HasData(
            new User { UserId = 1, Email = "hello@rawfin.net", PasswordHash = "AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==", Salt = "vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=", CreatedAt = DateTime.UtcNow },
            new User { UserId = 2, Email = "doe@email.net", PasswordHash = "AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==", Salt = "vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=", CreatedAt = DateTime.UtcNow },
            new User { UserId = 3, Email = "bob@email.net", PasswordHash = "AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==", Salt = "vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=", CreatedAt = DateTime.UtcNow }
        );

        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { UserId = 1, RoleId = 1 },
            new UserRole { UserId = 2, RoleId = 2 },
            new UserRole { UserId = 3, RoleId = 3 }
        );

        modelBuilder.Entity<UserDetail>().HasData(
            new UserDetail { UserDetailsId = 1, UserId = 1, Name = "Rawfin", InstituteName = "AIUB", IdCardNumber = "20-42459-1" }
        );

        modelBuilder.Entity<Question>().HasData(
            new Question { QuestionId = 1, Topic = "C# 12, Code Syntax, Maintainability", Explanation = "In C# 12, what are the advantages and trade-offs of using record types with pattern matching and deconstruction in ASP.NET 8 code, considering maintainability, readability, and potential performance implications?", UserId = 1, CreatedAt = DateTime.UtcNow },
            new Question { QuestionId = 2, Topic = "ASP.NET 8, HTTP Caching, Request Caching", Explanation = "With ASP.NET 8's improved request caching and HTTP caching strategies, in what scenarios could you effectively combine them to achieve optimal performance gains across different data access patterns (in-memory, database, external APIs)?", UserId = 1, CreatedAt = DateTime.UtcNow },
            new Question { QuestionId = 3, Topic = "ASP.NET 8, Development Workflow, Live Updates", Explanation = "What are the use cases for ASP.NET 8's hot reload capability, and how can it improve development workflow and reduce downtime in production environments?", UserId = 1, CreatedAt = DateTime.UtcNow },
            new Question { QuestionId = 4, Topic = "C#, Java, Developer Experience", Explanation = "As a C# developer comfortable with Microsoft ecosystem, is Spring Boot worth exploring even though it uses Java? When might switching make sense, if ever?", UserId = 1, CreatedAt = DateTime.UtcNow },
            new Question { QuestionId = 5, Topic = "ASP.NET, Spring Boot, Data Persistence", Explanation = "When working with diverse data sources and integration needs, how do ASP.NET Core's Entity Framework Core and Spring Boot's Spring Data JPA compare in terms of ease of use, performance, and integration capabilities?", UserId = 1, CreatedAt = DateTime.UtcNow },
            new Question { QuestionId = 6, Topic = "React Ecosystem, Mobile Apps, Desktop Apps", Explanation = "How can you adapt React development for building mobile apps with React Native, desktop applications with Electron, or server-side rendering with Next.js?", UserId = 1, CreatedAt = DateTime.UtcNow }
        );

        modelBuilder.Entity<Answer>().HasData(
            new Answer { AnswerId = 1, ParentAnswerId = 0, QuestionId = 1, Explanation = "In C# 12, using record types with pattern matching and deconstruction in ASP.NET 8 code enhances readability and maintainability by providing a concise syntax for defining immutable data types and simplifying comparisons and extraction of property values. This approach, inspired by functional programming, allows for more expressive and type-driven code, making it easier to add new rules or modify existing ones without extensive refactoring. However, the immutability of records may introduce overhead in scenarios where mutable objects are preferred, potentially affecting performance. Despite this, the benefits of using records, such as improved code clarity and built-in support for value-based equality, often outweigh the performance considerations, especially in projects that prioritize immutability and pattern matching.", UserId = 2, CreatedAt = DateTime.UtcNow },
            new Answer { AnswerId = 2, ParentAnswerId = 1, QuestionId = 1, Explanation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed, hendrerit?", UserId = 1, CreatedAt = DateTime.UtcNow },
            new Answer { AnswerId = 3, ParentAnswerId = 2, QuestionId = 1, Explanation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et.", UserId = 1, CreatedAt = DateTime.UtcNow },
            new Answer { AnswerId = 4, ParentAnswerId = 0, QuestionId = 1, Explanation = "Lorem ipsum dolor sit amet, consectetur adipiscing.", UserId = 2, CreatedAt = DateTime.UtcNow },
            new Answer { AnswerId = 5, ParentAnswerId = 4, QuestionId = 1, Explanation = "Lorem ipsum dolor sit amet.", UserId = 1, CreatedAt = DateTime.UtcNow }
        );
    }
}