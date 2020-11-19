using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAutomation.Migrations.TwitterStayInformd
{
    public partial class ChangedDateToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreatedAt",
                table: "Tweet",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Tweet",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
