using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectBlogCore.Migrations
{
    public partial class AlterandoTabelaUserClaimsV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "UsersClaims",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "UsersClaims",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersClaims_UserId1",
                table: "UsersClaims",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersClaims_Users_UserId1",
                table: "UsersClaims",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersClaims_Users_UserId1",
                table: "UsersClaims");

            migrationBuilder.DropIndex(
                name: "IX_UsersClaims_UserId1",
                table: "UsersClaims");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "UsersClaims");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UsersClaims");
        }
    }
}
