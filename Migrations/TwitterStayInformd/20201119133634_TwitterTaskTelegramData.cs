using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAutomation.Migrations.TwitterStayInformd
{
    public partial class TwitterTaskTelegramData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BodId",
                table: "TwitterTask",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChatId",
                table: "TwitterTask",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BodId",
                table: "TwitterTask");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "TwitterTask");
        }
    }
}
