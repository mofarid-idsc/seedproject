using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class RelationRolePermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserRoleRoleId",
                table: "Permissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserRoleUserId",
                table: "Permissions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_UserRoleUserId_UserRoleRoleId",
                table: "Permissions",
                columns: new[] { "UserRoleUserId", "UserRoleRoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_AspNetUserRoles_UserRoleUserId_UserRoleRoleId",
                table: "Permissions",
                columns: new[] { "UserRoleUserId", "UserRoleRoleId" },
                principalTable: "AspNetUserRoles",
                principalColumns: new[] { "UserId", "RoleId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_AspNetUserRoles_UserRoleUserId_UserRoleRoleId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_UserRoleUserId_UserRoleRoleId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "UserRoleRoleId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "UserRoleUserId",
                table: "Permissions");
        }
    }
}
