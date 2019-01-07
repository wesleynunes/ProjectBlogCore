using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectBlogCore.Migrations
{
    public partial class AlterandoTabelaUserClaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "UsersClaims");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "UsersClaims",
                nullable: false,
                defaultValue: "");
        }
    }
}
