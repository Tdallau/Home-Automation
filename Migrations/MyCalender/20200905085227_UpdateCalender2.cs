using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAutomation.Migrations.MyCalender
{
    public partial class UpdateCalender2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendar_Category_CalenderId",
                table: "Calendar");

            migrationBuilder.DropForeignKey(
                name: "FK_CalenderCategrory_Category_CategoryId1",
                table: "CalenderCategrory");

            migrationBuilder.DropIndex(
                name: "IX_CalenderCategrory_CategoryId1",
                table: "CalenderCategrory");

            migrationBuilder.DropIndex(
                name: "IX_Calendar_CalenderId",
                table: "Calendar");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "CalenderCategrory");

            migrationBuilder.DropColumn(
                name: "CalenderId",
                table: "Calendar");

            migrationBuilder.AddForeignKey(
                name: "FK_CalenderCategrory_Category_CalenderId",
                table: "CalenderCategrory",
                column: "CalenderId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalenderCategrory_Category_CalenderId",
                table: "CalenderCategrory");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "CalenderCategrory",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalenderId",
                table: "Calendar",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CalenderCategrory_CategoryId1",
                table: "CalenderCategrory",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_CalenderId",
                table: "Calendar",
                column: "CalenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendar_Category_CalenderId",
                table: "Calendar",
                column: "CalenderId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalenderCategrory_Category_CategoryId1",
                table: "CalenderCategrory",
                column: "CategoryId1",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
