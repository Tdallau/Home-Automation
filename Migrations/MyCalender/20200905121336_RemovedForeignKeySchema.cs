using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAutomation.Migrations.MyCalender
{
    public partial class RemovedForeignKeySchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalenderCategrory_Category_CalenderId",
                table: "CalenderCategrory");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "CalenderCategrory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CalenderCategrory_CategoryId1",
                table: "CalenderCategrory",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CalenderCategrory_Category_CategoryId1",
                table: "CalenderCategrory",
                column: "CategoryId1",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalenderCategrory_Category_CategoryId1",
                table: "CalenderCategrory");

            migrationBuilder.DropIndex(
                name: "IX_CalenderCategrory_CategoryId1",
                table: "CalenderCategrory");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "CalenderCategrory");

            migrationBuilder.AddForeignKey(
                name: "FK_CalenderCategrory_Category_CalenderId",
                table: "CalenderCategrory",
                column: "CalenderId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
