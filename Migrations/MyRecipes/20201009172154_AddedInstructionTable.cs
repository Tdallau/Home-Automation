using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HomeAutomation.Migrations.MyRecipes
{
    public partial class AddedInstructionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Instruction",
                table: "Recipe");

            migrationBuilder.CreateTable(
                name: "RecipeInstruction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecipeId = table.Column<int>(nullable: false),
                    Step = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeInstruction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeInstruction_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeInstruction_RecipeId",
                table: "RecipeInstruction",
                column: "RecipeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeInstruction");

            migrationBuilder.AddColumn<string>(
                name: "Instruction",
                table: "Recipe",
                type: "text",
                nullable: true);
        }
    }
}
