using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAutomation.Migrations.MyRecipes
{
    public partial class AddedInstructionTableList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RecipeInstruction_RecipeId",
                table: "RecipeInstruction");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeInstruction_RecipeId",
                table: "RecipeInstruction",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RecipeInstruction_RecipeId",
                table: "RecipeInstruction");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeInstruction_RecipeId",
                table: "RecipeInstruction",
                column: "RecipeId",
                unique: true);
        }
    }
}
