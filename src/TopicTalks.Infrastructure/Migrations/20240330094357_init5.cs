using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopicTalks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_UserRoles_UserRoleRoleId_UserRoleUserId",
                schema: "enum",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserRoles_UserRoleRoleId_UserRoleUserId",
                schema: "auth",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserRoleRoleId_UserRoleUserId",
                schema: "auth",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Roles_UserRoleRoleId_UserRoleUserId",
                schema: "enum",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "UserRoleRoleId",
                schema: "auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserRoleUserId",
                schema: "auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserRoleRoleId",
                schema: "enum",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UserRoleUserId",
                schema: "enum",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "UserRoles",
                newSchema: "auth");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                schema: "auth",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "auth",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                schema: "auth",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_RoleId",
                schema: "auth",
                table: "UserRoles");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                schema: "auth",
                newName: "UserRoles");

            migrationBuilder.AddColumn<long>(
                name: "UserRoleRoleId",
                schema: "auth",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserRoleUserId",
                schema: "auth",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserRoleRoleId",
                schema: "enum",
                table: "Roles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserRoleUserId",
                schema: "enum",
                table: "Roles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleRoleId_UserRoleUserId",
                schema: "auth",
                table: "Users",
                columns: new[] { "UserRoleRoleId", "UserRoleUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserRoleRoleId_UserRoleUserId",
                schema: "enum",
                table: "Roles",
                columns: new[] { "UserRoleRoleId", "UserRoleUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_UserRoles_UserRoleRoleId_UserRoleUserId",
                schema: "enum",
                table: "Roles",
                columns: new[] { "UserRoleRoleId", "UserRoleUserId" },
                principalTable: "UserRoles",
                principalColumns: new[] { "RoleId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserRoles_UserRoleRoleId_UserRoleUserId",
                schema: "auth",
                table: "Users",
                columns: new[] { "UserRoleRoleId", "UserRoleUserId" },
                principalTable: "UserRoles",
                principalColumns: new[] { "RoleId", "UserId" });
        }
    }
}
