using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAutomation.Migrations.TwitterStayInformd
{
    public partial class AddedTwitterTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TwitterTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    User = table.Column<string>(nullable: true),
                    CronTime = table.Column<string>(nullable: true),
                    NumberOfTweets = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TwitterTask", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TwitterTask");
        }
    }
}
