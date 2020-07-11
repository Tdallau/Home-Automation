using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAutomation.Migrations.MyShoppingList
{
    public partial class nameToName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "ShoppingGroup",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ShoppingGroup",
                newName: "name");
        }
    }
}
