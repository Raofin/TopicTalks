using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopicTalks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOtp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                schema: "auth",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Otps",
                schema: "auth",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "DATEADD(MINUTE, 5, GETDATE())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Otps", x => x.Email);
                });

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 14, 23, 49, 43, 440, DateTimeKind.Local).AddTicks(674));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 14, 23, 49, 43, 440, DateTimeKind.Local).AddTicks(677));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 14, 23, 49, 43, 440, DateTimeKind.Local).AddTicks(679));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 14, 23, 49, 43, 440, DateTimeKind.Local).AddTicks(680));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 14, 23, 49, 43, 440, DateTimeKind.Local).AddTicks(682));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 14, 23, 49, 43, 440, DateTimeKind.Local).AddTicks(557));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 14, 23, 49, 43, 440, DateTimeKind.Local).AddTicks(573));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 14, 23, 49, 43, 440, DateTimeKind.Local).AddTicks(586));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 14, 23, 49, 43, 440, DateTimeKind.Local).AddTicks(600));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 14, 23, 49, 43, 440, DateTimeKind.Local).AddTicks(613));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 14, 23, 49, 43, 440, DateTimeKind.Local).AddTicks(626));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 14, 23, 49, 43, 440, DateTimeKind.Local).AddTicks(161));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 14, 23, 49, 43, 440, DateTimeKind.Local).AddTicks(177));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 14, 23, 49, 43, 440, DateTimeKind.Local).AddTicks(179));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Otps",
                schema: "auth");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                schema: "auth",
                table: "Users");

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 5, 7, 1, 47, 303, DateTimeKind.Local).AddTicks(3284));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 5, 7, 1, 47, 303, DateTimeKind.Local).AddTicks(3286));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 5, 7, 1, 47, 303, DateTimeKind.Local).AddTicks(3288));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 5, 7, 1, 47, 303, DateTimeKind.Local).AddTicks(3289));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 5, 7, 1, 47, 303, DateTimeKind.Local).AddTicks(3291));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 5, 7, 1, 47, 303, DateTimeKind.Local).AddTicks(3166));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 5, 7, 1, 47, 303, DateTimeKind.Local).AddTicks(3201));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 5, 7, 1, 47, 303, DateTimeKind.Local).AddTicks(3214));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 5, 7, 1, 47, 303, DateTimeKind.Local).AddTicks(3226));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 5, 7, 1, 47, 303, DateTimeKind.Local).AddTicks(3237));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 5, 7, 1, 47, 303, DateTimeKind.Local).AddTicks(3248));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 5, 7, 1, 47, 303, DateTimeKind.Local).AddTicks(2808));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 5, 7, 1, 47, 303, DateTimeKind.Local).AddTicks(2823));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 5, 7, 1, 47, 303, DateTimeKind.Local).AddTicks(2825));
        }
    }
}
