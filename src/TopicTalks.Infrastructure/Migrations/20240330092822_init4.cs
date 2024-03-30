using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopicTalks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
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
                name: "Answers",
                schema: "post",
                columns: table => new
                {
                    AnswerId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentAnswerId = table.Column<long>(type: "bigint", nullable: true, defaultValue: 0L),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerId);
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
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "enum",
                columns: table => new
                {
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserRoleRoleId = table.Column<long>(type: "bigint", nullable: true),
                    UserRoleUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
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
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "enum",
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
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
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UserRoleRoleId = table.Column<long>(type: "bigint", nullable: true),
                    UserRoleUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_UserRoleRoleId_UserRoleUserId",
                        columns: x => new { x.UserRoleRoleId, x.UserRoleUserId },
                        principalTable: "UserRoles",
                        principalColumns: new[] { "RoleId", "UserId" });
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
                name: "IX_Roles_UserRoleRoleId_UserRoleUserId",
                schema: "enum",
                table: "Roles",
                columns: new[] { "UserRoleRoleId", "UserRoleUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_UserId",
                schema: "auth",
                table: "UserDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleRoleId_UserRoleUserId",
                schema: "auth",
                table: "Users",
                columns: new[] { "UserRoleRoleId", "UserRoleUserId" });

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
                name: "FK_Questions_Users",
                schema: "post",
                table: "Questions",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_UserRoles_UserRoleRoleId_UserRoleUserId",
                schema: "enum",
                table: "Roles",
                columns: new[] { "UserRoleRoleId", "UserRoleUserId" },
                principalTable: "UserRoles",
                principalColumns: new[] { "RoleId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_Users",
                schema: "auth",
                table: "UserDetails",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_UserRoles_UserRoleRoleId_UserRoleUserId",
                schema: "enum",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "Answers",
                schema: "post");

            migrationBuilder.DropTable(
                name: "Questions",
                schema: "post");

            migrationBuilder.DropTable(
                name: "UserDetails",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "enum");
        }
    }
}
