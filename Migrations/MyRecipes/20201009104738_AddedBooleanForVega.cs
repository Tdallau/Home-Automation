using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAutomation.Migrations.MyRecipes
{
    public partial class AddedBooleanForVega : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Vega",
                table: "Recipe",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vega",
                table: "Recipe");
        }
    }
}
