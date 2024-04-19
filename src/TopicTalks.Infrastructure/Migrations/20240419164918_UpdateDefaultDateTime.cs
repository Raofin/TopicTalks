using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopicTalks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDefaultDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "auth",
                table: "Users",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "post",
                table: "Questions",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiresAt",
                schema: "auth",
                table: "Otps",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "dateadd(minute, 5, getutcdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "DATEADD(MINUTE, 5, GETDATE())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "post",
                table: "Answers",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "auth",
                table: "Users",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getutcdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "post",
                table: "Questions",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getutcdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiresAt",
                schema: "auth",
                table: "Otps",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "DATEADD(MINUTE, 5, GETDATE())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "dateadd(minute, 5, getutcdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "post",
                table: "Answers",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getutcdate())");

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
    }
}
