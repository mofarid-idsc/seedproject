using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class UpdatePermissionsV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "FunctionName",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "ObjectName",
                table: "Permissions");

            migrationBuilder.AddColumn<string>(
                name: "ActionName",
                table: "Permissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ControllerName",
                table: "Permissions",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "Permissions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserRoleRoleId",
                table: "Permissions",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_AspNetUserRoles_UserRoleRoleId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_UserRoleRoleId",
                table: "Permissions");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "ActionName",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "ControllerName",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "UserRoleRoleId",
                table: "Permissions");

            migrationBuilder.AddColumn<string>(
                name: "FunctionName",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Permissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ObjectName",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");
        }
    }
}
