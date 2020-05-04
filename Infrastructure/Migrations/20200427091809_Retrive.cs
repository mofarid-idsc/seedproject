using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Retrive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "GroupID",
                table: "Permissions",
                newName: "GroupId");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "Permissions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Permissions",
                newName: "GroupID");

            migrationBuilder.AlterColumn<string>(
                name: "GroupID",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<string>(
                name: "UserRoleRoleId",
                table: "Permissions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserRoleUserId",
                table: "Permissions",
                type: "nvarchar(450)",
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
    }
}
