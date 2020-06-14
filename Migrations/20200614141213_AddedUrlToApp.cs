using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAutomation.Migrations
{
    public partial class AddedUrlToApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "App",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "App");
        }
    }
}
