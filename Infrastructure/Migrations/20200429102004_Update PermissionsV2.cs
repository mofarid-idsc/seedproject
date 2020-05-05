using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class UpdatePermissionsV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_AspNetUserRoles_UserRoleRoleId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_UserRoleRoleId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "UserRoleRoleId",
                table: "Permissions");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "Permissions",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_AspNetUserRoles_RoleId",
                table: "Permissions",
                column: "RoleId",
                principalTable: "AspNetUserRoles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_AspNetUserRoles_RoleId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "Permissions",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserRoleRoleId",
                table: "Permissions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_UserRoleRoleId",
                table: "Permissions",
                column: "UserRoleRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_AspNetUserRoles_UserRoleRoleId",
                table: "Permissions",
                column: "UserRoleRoleId",
                principalTable: "AspNetUserRoles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
