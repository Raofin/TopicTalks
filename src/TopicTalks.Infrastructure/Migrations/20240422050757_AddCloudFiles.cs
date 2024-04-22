using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopicTalks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCloudFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.CreateTable(
                name: "CloudFiles",
                schema: "core",
                columns: table => new
                {
                    CloudFileId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    WebContentLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    WebViewLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DirectLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloudFiles", x => x.CloudFileId);
                    table.ForeignKey(
                        name: "FK_CloudFiles_Users",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 22, 5, 7, 57, 175, DateTimeKind.Utc).AddTicks(461));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 22, 5, 7, 57, 175, DateTimeKind.Utc).AddTicks(464));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 22, 5, 7, 57, 175, DateTimeKind.Utc).AddTicks(465));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 22, 5, 7, 57, 175, DateTimeKind.Utc).AddTicks(467));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 22, 5, 7, 57, 175, DateTimeKind.Utc).AddTicks(470));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 22, 5, 7, 57, 175, DateTimeKind.Utc).AddTicks(418));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 22, 5, 7, 57, 175, DateTimeKind.Utc).AddTicks(420));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 22, 5, 7, 57, 175, DateTimeKind.Utc).AddTicks(421));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 22, 5, 7, 57, 175, DateTimeKind.Utc).AddTicks(423));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 22, 5, 7, 57, 175, DateTimeKind.Utc).AddTicks(424));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 22, 5, 7, 57, 175, DateTimeKind.Utc).AddTicks(426));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 22, 5, 7, 57, 175, DateTimeKind.Utc).AddTicks(251));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 22, 5, 7, 57, 175, DateTimeKind.Utc).AddTicks(255));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 22, 5, 7, 57, 175, DateTimeKind.Utc).AddTicks(257));

            migrationBuilder.CreateIndex(
                name: "IX_CloudFiles_UserId",
                schema: "core",
                table: "CloudFiles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CloudFiles",
                schema: "core");

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 19, 16, 49, 16, 766, DateTimeKind.Utc).AddTicks(5904));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 19, 16, 49, 16, 766, DateTimeKind.Utc).AddTicks(5906));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 19, 16, 49, 16, 766, DateTimeKind.Utc).AddTicks(5909));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 19, 16, 49, 16, 766, DateTimeKind.Utc).AddTicks(5911));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 19, 16, 49, 16, 766, DateTimeKind.Utc).AddTicks(5912));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 19, 16, 49, 16, 766, DateTimeKind.Utc).AddTicks(5867));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 19, 16, 49, 16, 766, DateTimeKind.Utc).AddTicks(5869));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 19, 16, 49, 16, 766, DateTimeKind.Utc).AddTicks(5870));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 19, 16, 49, 16, 766, DateTimeKind.Utc).AddTicks(5872));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 19, 16, 49, 16, 766, DateTimeKind.Utc).AddTicks(5874));

            migrationBuilder.UpdateData(
                schema: "post",
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 19, 16, 49, 16, 766, DateTimeKind.Utc).AddTicks(5875));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 19, 16, 49, 16, 766, DateTimeKind.Utc).AddTicks(5681));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 19, 16, 49, 16, 766, DateTimeKind.Utc).AddTicks(5687));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 19, 16, 49, 16, 766, DateTimeKind.Utc).AddTicks(5785));
        }
    }
}
