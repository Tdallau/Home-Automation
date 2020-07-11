using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAutomation.Migrations.MyShoppingList
{
    public partial class addedActiveToShoppingGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ShoppingGroupUser",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "ShoppingGroupUser");
        }
    }
}
