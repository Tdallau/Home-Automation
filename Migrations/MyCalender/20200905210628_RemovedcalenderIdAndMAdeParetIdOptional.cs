using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAutomation.Migrations.MyCalender
{
    public partial class RemovedcalenderIdAndMAdeParetIdOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalenderCategrory_Calendar_CategoryId",
                table: "CalenderCategrory");

            migrationBuilder.DropForeignKey(
                name: "FK_CalenderCategrory_Category_CategoryId1",
                table: "CalenderCategrory");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_ParentId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_CalenderCategrory_CategoryId1",
                table: "CalenderCategrory");

            migrationBuilder.DropColumn(
                name: "CalenderId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "CalenderCategrory");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "Category",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_CalenderCategrory_Calendar_CalenderId",
                table: "CalenderCategrory",
                column: "CalenderId",
                principalTable: "Calendar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CalenderCategrory_Category_CategoryId",
                table: "CalenderCategrory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentId",
                table: "Category",
                column: "ParentId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalenderCategrory_Calendar_CalenderId",
                table: "CalenderCategrory");

            migrationBuilder.DropForeignKey(
                name: "FK_CalenderCategrory_Category_CategoryId",
                table: "CalenderCategrory");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_ParentId",
                table: "Category");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "Category",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalenderId",
                table: "Category",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "CalenderCategrory",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CalenderCategrory_CategoryId1",
                table: "CalenderCategrory",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CalenderCategrory_Calendar_CategoryId",
                table: "CalenderCategrory",
                column: "CategoryId",
                principalTable: "Calendar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CalenderCategrory_Category_CategoryId1",
                table: "CalenderCategrory",
                column: "CategoryId1",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentId",
                table: "Category",
                column: "ParentId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
