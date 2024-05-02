using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TopicTalks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "post");

            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.EnsureSchema(
                name: "enum");

            migrationBuilder.CreateTable(
                name: "LogEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Otps",
                schema: "auth",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "dateAdd(minute, 5, getUtcDate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Otps", x => x.Email);
                });

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
                name: "Answers",
                schema: "post",
                columns: table => new
                {
                    AnswerId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentAnswerId = table.Column<long>(type: "bigint", nullable: true, defaultValue: 0L),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsNotified = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getUtcDate()"),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerId);
                });

            migrationBuilder.CreateTable(
                name: "CloudFiles",
                schema: "core",
                columns: table => new
                {
                    CloudFileId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false, comment: "Bytes"),
                    WebContentLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    WebViewLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DirectLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getUtcDate()"),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloudFiles", x => x.CloudFileId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "auth",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getUtcDate()"),
                    ImageFileId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_CloudFiles_ImageFileId",
                        column: x => x.ImageFileId,
                        principalSchema: "core",
                        principalTable: "CloudFiles",
                        principalColumn: "CloudFileId",
                        onDelete: ReferentialAction.SetNull);
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
                    IsNotified = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getUtcDate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    ImageFileId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_CloudFiles_ImageFileId",
                        column: x => x.ImageFileId,
                        principalSchema: "core",
                        principalTable: "CloudFiles",
                        principalColumn: "CloudFileId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Questions_Users_UserId",
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
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    InstituteName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IdCardNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.UserDetailsId);
                    table.ForeignKey(
                        name: "FK_UserDetails_Users_UserId",
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
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "enum",
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
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
                columns: new[] { "UserId", "CreatedAt", "Email", "ImageFileId", "IsVerified", "PasswordHash", "Salt", "Username" },
                values: new object[] { 1L, new DateTime(2024, 1, 27, 13, 13, 46, 462, DateTimeKind.Utc).AddTicks(5219), "hello@rawfin.net", null, true, "AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==", "vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=", "Rawfin" });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "ImageFileId", "PasswordHash", "Salt", "Username" },
                values: new object[,]
                {
                    { 2L, new DateTime(2024, 1, 27, 13, 13, 46, 462, DateTimeKind.Utc).AddTicks(5226), "doe@topictalks.net", null, "AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==", "vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=", "Doe" },
                    { 3L, new DateTime(2024, 1, 27, 13, 13, 46, 462, DateTimeKind.Utc).AddTicks(5229), "bob@topictalks.net", null, "AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==", "vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=", "Bob" },
                    { 4L, new DateTime(2024, 1, 27, 13, 13, 46, 462, DateTimeKind.Utc).AddTicks(5231), "oec@topictalks.net", null, "AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==", "vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=", "Oweo" },
                    { 5L, new DateTime(2024, 1, 27, 13, 13, 46, 462, DateTimeKind.Utc).AddTicks(5233), "eor@topictalks.net", null, "AQAAAAIAAYagAAAAEH4sN4yXGhfbr83UweaRK6lW4ql9PztpEKWTR6SbkhWTiX1P0mWxRTm8gJr8O3SENg==", "vFsYhyBIKKEYbGH4F5rQfR2Q5bAyZ4nH2Q0Vwo3kxxM=", "Eorc" }
                });

            migrationBuilder.InsertData(
                schema: "post",
                table: "Questions",
                columns: new[] { "QuestionId", "CreatedAt", "Explanation", "ImageFileId", "IsNotified", "Topic", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 4, 5, 7, 1, 0, 0, DateTimeKind.Unspecified), "What is the difference between Raspbian and other operating systems available for Raspberry Pi, like Ubuntu or Arch Linux?", null, true, "Raspberry Pi, Operating System, Raspbian", null, 1L },
                    { 2L, new DateTime(2024, 4, 4, 7, 54, 0, 0, DateTimeKind.Unspecified), "As a C# developer comfortable with Microsoft ecosystem, is Spring Boot worth exploring even though it uses Java? When might switching make sense, if ever?", null, true, "C#, Java, Spring Boot, Developer Experience", null, 4L },
                    { 3L, new DateTime(2024, 3, 25, 8, 43, 0, 0, DateTimeKind.Unspecified), "How does the use of timeouts help in preventing or resolving deadlocks?", null, true, "Deadlocks, Operating System", null, 1L },
                    { 4L, new DateTime(2024, 3, 21, 5, 13, 0, 0, DateTimeKind.Unspecified), "How do the transport layer and network layer protocols, such as TCP and IP, facilitate communication between processes in a network application?", null, true, "Computer Networks, TCP/IP, Network Layer Protocols", null, 1L },
                    { 5L, new DateTime(2024, 3, 10, 11, 46, 0, 0, DateTimeKind.Unspecified), "What is a transaction, and why is ACID compliance important in database management?", null, true, "RDBMS, Transaction, ACID, Database Management", null, 5L },
                    { 6L, new DateTime(2024, 2, 27, 8, 27, 0, 0, DateTimeKind.Unspecified), "What's the difference between a Deterministic Finite Automaton (DFA) and a Nondeterministic Finite Automaton (NFA)?", null, true, "HTTP, FTP, SMTP, Internet Communication", null, 1L },
                    { 7L, new DateTime(2024, 2, 16, 6, 16, 0, 0, DateTimeKind.Unspecified), "How does the V-model integrate testing activities into each phase of the development process?", null, true, "V-model, Testing, SDLC", null, 5L },
                    { 8L, new DateTime(2024, 2, 12, 5, 18, 0, 0, DateTimeKind.Unspecified), "Can Arduino boards communicate with each other or with other devices, and if so, how?", null, true, "Arduino, I2C, SPI, Embedded Systems", null, 1L },
                    { 9L, new DateTime(2024, 1, 25, 5, 25, 0, 0, DateTimeKind.Unspecified), "How does the Domain Name System (DNS) work, and why is it important for navigating the internet?", null, true, "Domain Name System, DNS, Computer Networks", null, 4L },
                    { 10L, new DateTime(2024, 1, 17, 7, 1, 0, 0, DateTimeKind.Unspecified), "How do Hibernate's HQL (Hibernate Query Language) and Entity Framework's LINQ (Language Integrated Query) compare in terms of syntax and functionality for executing database queries?", null, true, "Hibernate, HQL, Entity Framework, LINQ, SQL", null, 1L }
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "UserDetails",
                columns: new[] { "UserDetailsId", "FullName", "IdCardNumber", "InstituteName", "UserId" },
                values: new object[,]
                {
                    { 1L, "Zaid Amin Rawfin", "20-42459-1", "AIUB", 1L },
                    { 2L, "Oweo Yec Wev", "2020-55-3361", "QWDA", 4L },
                    { 3L, "Voer Eor Oec", "3-17655614-43", "CREX", 5L }
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 2L, 2L },
                    { 3L, 3L }
                });

            migrationBuilder.InsertData(
                schema: "post",
                table: "Answers",
                columns: new[] { "AnswerId", "CreatedAt", "Explanation", "IsNotified", "ParentAnswerId", "QuestionId", "UserId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 4, 5, 7, 10, 0, 0, DateTimeKind.Unspecified), "Raspberry Pi OS (formerly known as Raspbian) is a Linux distribution specifically designed for the Raspberry Pi, based on Debian. It's optimized for the Pi's hardware, offering a lightweight desktop environment that's well-suited for its low-powered platform. Raspberry Pi OS includes special programs and kernel modules for HAT and additional hardware support, which other operating systems like Ubuntu or Arch Linux might not have. While Ubuntu and Arch Linux are also Linux distributions, they are not specifically tailored for the Raspberry Pi and may require modifications to work with its unique hardware and software environment. Therefore, Raspberry Pi OS is often the preferred choice for users looking for a seamless experience with their Raspberry Pi devices.", true, 0L, 1L, 2L },
                    { 2L, new DateTime(2024, 4, 5, 7, 25, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et fermentum dui. Ut orci quam, ornare sed lorem sed, hendrerit?", true, 1L, 1L, 1L },
                    { 3L, new DateTime(2024, 4, 5, 7, 34, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam et.", true, 2L, 1L, 4L },
                    { 4L, new DateTime(2024, 4, 5, 7, 46, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing.", true, 0L, 1L, 2L },
                    { 5L, new DateTime(2024, 4, 5, 7, 59, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet.", true, 4L, 1L, 5L }
                });

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
                name: "IX_CloudFiles_UserId",
                schema: "core",
                table: "CloudFiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ImageFileId",
                schema: "post",
                table: "Questions",
                column: "ImageFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_UserId",
                schema: "post",
                table: "Questions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_UserId",
                schema: "auth",
                table: "UserDetails",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "auth",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                schema: "auth",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                schema: "auth",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ImageFileId",
                schema: "auth",
                table: "Users",
                column: "ImageFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                schema: "post",
                table: "Answers",
                column: "QuestionId",
                principalSchema: "post",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Users",
                schema: "post",
                table: "Answers",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CloudFiles_Users_UserId",
                schema: "core",
                table: "CloudFiles",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CloudFiles_Users_UserId",
                schema: "core",
                table: "CloudFiles");

            migrationBuilder.DropTable(
                name: "Answers",
                schema: "post");

            migrationBuilder.DropTable(
                name: "LogEvents");

            migrationBuilder.DropTable(
                name: "Otps",
                schema: "auth");

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

            migrationBuilder.DropTable(
                name: "CloudFiles",
                schema: "core");
        }
    }
}
