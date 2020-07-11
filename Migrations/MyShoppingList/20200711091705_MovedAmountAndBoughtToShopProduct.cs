using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAutomation.Migrations.MyShoppingList
{
    public partial class MovedAmountAndBoughtToShopProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Bought",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "Amount",
                table: "ShopProduct",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Bought",
                table: "ShopProduct",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "ShopProduct");

            migrationBuilder.DropColumn(
                name: "Bought",
                table: "ShopProduct");

            migrationBuilder.AddColumn<string>(
                name: "Amount",
                table: "Product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Bought",
                table: "Product",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
