using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAutomation.Migrations
{
    public partial class AddedDefaultApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Default",
                table: "UserApp",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Default",
                table: "UserApp");
        }
    }
}
