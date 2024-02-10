using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OSL.DAL.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "post");

            migrationBuilder.EnsureSchema(
                name: "enum");

            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "enum",
                columns: table => new
                {
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "auth",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                schema: "post",
                columns: table => new
                {
                    QuestionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_Users",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserDetails",
                schema: "auth",
                columns: table => new
                {
                    UserDetailsId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    InstituteName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IdCardNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.UserDetailsId);
                    table.ForeignKey(
                        name: "FK_UserDetails_Users",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "auth",
                columns: table => new
                {
                    UserRoleId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    RoleId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles",
                        column: x => x.RoleId,
                        principalSchema: "enum",
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                schema: "post",
                columns: table => new
                {
                    AnswerId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentAnswerId = table.Column<long>(type: "bigint", nullable: true, defaultValue: 0L),
                    QuestionId = table.Column<long>(type: "bigint", nullable: true),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answers_Questions",
                        column: x => x.QuestionId,
                        principalSchema: "post",
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Answers_Users",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                schema: "enum",
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1L, "Student" },
                    { 2L, "Teacher" },
                    { 3L, "Moderator" }
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "PasswordHash", "Salt" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 2, 11, 4, 55, 52, 977, DateTimeKind.Local).AddTicks(838), "hello@rawfin.net", "AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==", "vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=" },
                    { 2L, new DateTime(2024, 2, 11, 4, 55, 52, 977, DateTimeKind.Local).AddTicks(853), "doe@email.net", "AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==", "vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=" },
                    { 3L, new DateTime(2024, 2, 11, 4, 55, 52, 977, DateTimeKind.Local).AddTicks(856), "bob@email.net", "AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==", "vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=" }
                });

            migrationBuilder.InsertData(
                schema: "post",
                table: "Questions",
                columns: new[] { "QuestionId", "CreatedAt", "Explanation", "Topic", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 2, 11, 4, 55, 52, 977, DateTimeKind.Local).AddTicks(1146), "In C# 12, what are the advantages and trade-offs of using record types with pattern matching and deconstruction in ASP.NET 8 code, considering maintainability, readability, and potential performance implications?", "C# 12, Code Syntax, Maintainability", null, 1L },
                    { 2L, new DateTime(2024, 2, 11, 4, 55, 52, 977, DateTimeKind.Local).AddTicks(1154), "With ASP.NET 8's improved request caching and HTTP caching strategies, in what scenarios could you effectively combine them to achieve optimal performance gains across different data access patterns (in-memory, database, external APIs)?", "ASP.NET 8, HTTP Caching, Request Caching", null, 1L },
                    { 3L, new DateTime(2024, 2, 11, 4, 55, 52, 977, DateTimeKind.Local).AddTicks(1156), "What are the use cases for ASP.NET 8's hot reload capability, and how can it improve development workflow and reduce downtime in production environments?", "ASP.NET 8, Development Workflow, Live Updates", null, 1L },
                    { 4L, new DateTime(2024, 2, 11, 4, 55, 52, 977, DateTimeKind.Local).AddTicks(1158), "As a C# developer comfortable with Microsoft ecosystem, is Spring Boot worth exploring even though it uses Java? When might switching make sense, if ever?", "C#, Java, Developer Experience", null, 1L },
                    { 5L, new DateTime(2024, 2, 11, 4, 55, 52, 977, DateTimeKind.Local).AddTicks(1159), "When working with diverse data sources and integration needs, how do ASP.NET Core's Entity Framework Core and Spring Boot's Spring Data JPA compare in terms of ease of use, performance, and integration capabilities?", "ASP.NET, Spring Boot, Data Persistence", null, 1L },
                    { 6L, new DateTime(2024, 2, 11, 4, 55, 52, 977, DateTimeKind.Local).AddTicks(1161), "How can you adapt React development for building mobile apps with React Native, desktop applications with Electron, or server-side rendering with Next.js?", "React Ecosystem, Mobile Apps, Desktop Apps", null, 1L }
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "UserDetails",
                columns: new[] { "UserDetailsId", "IdCardNumber", "InstituteName", "Name", "UserId" },
                values: new object[] { 1L, "20-42459-1", "AIUB", "Rawfin", 1L });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "UserRoles",
                columns: new[] { "UserRoleId", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1L, 1L, 1L },
                    { 2L, 2L, 2L },
                    { 3L, 3L, 3L }
                });

            migrationBuilder.InsertData(
                schema: "post",
                table: "Answers",
                columns: new[] { "AnswerId", "CreatedAt", "Explanation", "ParentAnswerId", "QuestionId", "UserId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 2, 11, 4, 55, 52, 977, DateTimeKind.Local).AddTicks(1202), "ASP.NET 8, with the introduction of C# 12's record types, pattern matching, and deconstruction, presents a nuanced landscape for developers. The advantages are evident, as records facilitate encapsulated immutability, promoting cleaner code and reducing potential errors. Deconstructing records enhances readability, while pattern matching introduces expressive conditionals. Performance gains are plausible, with optimized equality checks and accelerated property access. However, the learning curve associated with these features and the potential inflexibility for complex data modifications pose challenges. Moreover, records lack direct inheritance support, requiring adjustments to established design patterns. In conclusion, while records and pattern matching offer significant advantages in terms of immutability and readability, a thoughtful evaluation of trade-offs is imperative. The decision to adopt these features should align with the project's goals, team's expertise, and the potential learning curve, ensuring a judicious integration without compromising project success.", 0L, 1L, 2L },
                    { 2L, new DateTime(2024, 2, 11, 4, 55, 52, 977, DateTimeKind.Local).AddTicks(1204), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed, hendrerit?", 1L, 1L, 1L },
                    { 3L, new DateTime(2024, 2, 11, 4, 55, 52, 977, DateTimeKind.Local).AddTicks(1308), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et.", 2L, 1L, 1L },
                    { 4L, new DateTime(2024, 2, 11, 4, 55, 52, 977, DateTimeKind.Local).AddTicks(1311), "Lorem ipsum dolor sit amet, consectetur adipiscing .", 0L, 1L, 2L },
                    { 5L, new DateTime(2024, 2, 11, 4, 55, 52, 977, DateTimeKind.Local).AddTicks(1312), "Lorem ipsum dolor sit amet.", 4L, 1L, 2L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_ParentAnswerId",
                schema: "post",
                table: "Answers",
                column: "ParentAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                schema: "post",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_UserId",
                schema: "post",
                table: "Answers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_UserId",
                schema: "post",
                table: "Questions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_UserId",
                schema: "auth",
                table: "UserDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "auth",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                schema: "auth",
                table: "UserRoles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers",
                schema: "post");

            migrationBuilder.DropTable(
                name: "UserDetails",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "Questions",
                schema: "post");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "enum");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "auth");
        }
    }
}
