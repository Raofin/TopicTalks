using Microsoft.EntityFrameworkCore;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Enums;

namespace TopicTalks.Infrastructure.Persistence;

public static class SeedData
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { RoleId = (long)RoleType.Student, RoleName = nameof(RoleType.Student) },
            new Role { RoleId = (long)RoleType.Teacher, RoleName = nameof(RoleType.Teacher) },
            new Role { RoleId = (long)RoleType.Moderator, RoleName = nameof(RoleType.Moderator) }
        );

        modelBuilder.Entity<User>().HasData(
            new User { UserId = 1, Username = "Rawfin", Email = "hello@rawfin.net", PasswordHash = "AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==", Salt = "vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=", IsVerified = true, CreatedAt = DateTime.UtcNow.AddDays(-96) },
            new User { UserId = 2, Username = "Doe", Email = "doe@topictalks.net", PasswordHash = "AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==", Salt = "vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=", IsVerified = false, CreatedAt = DateTime.UtcNow.AddDays(-96) },
            new User { UserId = 3, Username = "Bob", Email = "bob@topictalks.net", PasswordHash = "AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==", Salt = "vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=", IsVerified = false, CreatedAt = DateTime.UtcNow.AddDays(-96) },
            new User { UserId = 4, Username = "Oweo", Email = "oec@topictalks.net", PasswordHash = "AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==", Salt = "vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=", IsVerified = false, CreatedAt = DateTime.UtcNow.AddDays(-96) },
            new User { UserId = 5, Username = "Eorc", Email = "eor@topictalks.net", PasswordHash = "AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==", Salt = "vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=", IsVerified = false, CreatedAt = DateTime.UtcNow.AddDays(-96) }
        );

        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { UserId = 1, RoleId = 1 },
            new UserRole { UserId = 2, RoleId = 2 },
            new UserRole { UserId = 3, RoleId = 3 },
            new UserRole { UserId = 4, RoleId = 1 },
            new UserRole { UserId = 5, RoleId = 1 }
        );

        modelBuilder.Entity<UserDetail>().HasData(
            new UserDetail { UserDetailsId = 1, UserId = 1, FullName = "Zaid Amin Rawfin", InstituteName = "AIUB", IdCardNumber = "20-42459-1" },
            new UserDetail { UserDetailsId = 2, UserId = 4, FullName = "Oweo Yec Wev", InstituteName = "QWDA", IdCardNumber = "2020-55-3361" },
            new UserDetail { UserDetailsId = 3, UserId = 5, FullName = "Voer Eor Oec", InstituteName = "CREX", IdCardNumber = "3-17655614-43" }
        );

        modelBuilder.Entity<Question>().HasData(
            new Question { QuestionId = 1, UserId = 1, CreatedAt = new DateTime(2024, 4, 5, 7, 1, 0), Topic = "Raspberry Pi, Operating System, Raspbian", Explanation = "What is the difference between Raspbian and other operating systems available for Raspberry Pi, like Ubuntu or Arch Linux?" },
            new Question { QuestionId = 2, UserId = 4, CreatedAt = new DateTime(2024, 4, 4, 7, 54, 0), Topic = "C#, Java, Spring Boot, Developer Experience", Explanation = "As a C# developer comfortable with Microsoft ecosystem, is Spring Boot worth exploring even though it uses Java? When might switching make sense, if ever?" },
            new Question { QuestionId = 3, UserId = 1, CreatedAt = new DateTime(2024, 3, 25, 8, 43, 0), Topic = "Deadlocks, Operating System", Explanation = "How does the use of timeouts help in preventing or resolving deadlocks?" },
            new Question { QuestionId = 4, UserId = 1, CreatedAt = new DateTime(2024, 3, 21, 5, 13, 0), Topic = "Computer Networks, TCP/IP, Network Layer Protocols", Explanation = "How do the transport layer and network layer protocols, such as TCP and IP, facilitate communication between processes in a network application?" },
            new Question { QuestionId = 5, UserId = 5, CreatedAt = new DateTime(2024, 3, 10, 11, 46, 0), Topic = "RDBMS, Transaction, ACID, Database Management", Explanation = "What is a transaction, and why is ACID compliance important in database management?" },
            new Question { QuestionId = 6, UserId = 1, CreatedAt = new DateTime(2024, 2, 27, 8, 27, 0), Topic = "HTTP, FTP, SMTP, Internet Communication", Explanation = "What's the difference between a Deterministic Finite Automaton (DFA) and a Nondeterministic Finite Automaton (NFA)?" },
            new Question { QuestionId = 7, UserId = 5, CreatedAt = new DateTime(2024, 2, 16, 6, 16, 0), Topic = "V-model, Testing, SDLC", Explanation = "How does the V-model integrate testing activities into each phase of the development process?" },
            new Question { QuestionId = 8, UserId = 1, CreatedAt = new DateTime(2024, 2, 12, 5, 18, 0), Topic = "Arduino, I2C, SPI, Embedded Systems", Explanation = "Can Arduino boards communicate with each other or with other devices, and if so, how?" },
            new Question { QuestionId = 9, UserId = 4, CreatedAt = new DateTime(2024, 1, 25, 5, 25, 0), Topic = "Domain Name System, DNS, Computer Networks", Explanation = "How does the Domain Name System (DNS) work, and why is it important for navigating the internet?" },
            new Question { QuestionId = 10, UserId = 1, CreatedAt = new DateTime(2024, 1, 17, 7, 1, 0), Topic = "Hibernate, HQL, Entity Framework, LINQ, SQL", Explanation = "How do Hibernate's HQL (Hibernate Query Language) and Entity Framework's LINQ (Language Integrated Query) compare in terms of syntax and functionality for executing database queries?" }
        );

        modelBuilder.Entity<Answer>().HasData(
            new Answer { AnswerId = 1, ParentAnswerId = 0, QuestionId = 1, UserId = 2, CreatedAt = new DateTime(2024, 4, 5, 7, 10, 0), Explanation = "Raspberry Pi OS (formerly known as Raspbian) is a Linux distribution specifically designed for the Raspberry Pi, based on Debian. It's optimized for the Pi's hardware, offering a lightweight desktop environment that's well-suited for its low-powered platform. Raspberry Pi OS includes special programs and kernel modules for HAT and additional hardware support, which other operating systems like Ubuntu or Arch Linux might not have. While Ubuntu and Arch Linux are also Linux distributions, they are not specifically tailored for the Raspberry Pi and may require modifications to work with its unique hardware and software environment. Therefore, Raspberry Pi OS is often the preferred choice for users looking for a seamless experience with their Raspberry Pi devices." },
            new Answer { AnswerId = 2, ParentAnswerId = 1, QuestionId = 1, UserId = 1, CreatedAt = new DateTime(2024, 4, 5, 7, 25, 0), Explanation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed, hendrerit?" },
            new Answer { AnswerId = 3, ParentAnswerId = 2, QuestionId = 1, UserId = 4, CreatedAt = new DateTime(2024, 4, 5, 7, 34, 0), Explanation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et." },
            new Answer { AnswerId = 4, ParentAnswerId = 0, QuestionId = 1, UserId = 2, CreatedAt = new DateTime(2024, 4, 5, 7, 46, 0), Explanation = "Lorem ipsum dolor sit amet, consectetur adipiscing." },
            new Answer { AnswerId = 5, ParentAnswerId = 4, QuestionId = 1, UserId = 5, CreatedAt = new DateTime(2024, 4, 5, 7, 59, 0), Explanation = "Lorem ipsum dolor sit amet." }
        );
    }
}