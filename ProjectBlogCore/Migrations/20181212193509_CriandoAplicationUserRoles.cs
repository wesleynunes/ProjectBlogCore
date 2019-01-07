using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectBlogCore.Migrations
{
    public partial class CriandoAplicationUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "UsersRoles",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "UsersRoles");
        }
    }
}
