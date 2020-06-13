using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAutomation.Migrations
{
    public partial class tokenHasConnectionToApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppId",
                table: "UserToken",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_AppId",
                table: "UserToken",
                column: "AppId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserToken_App_AppId",
                table: "UserToken",
                column: "AppId",
                principalTable: "App",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserToken_App_AppId",
                table: "UserToken");

            migrationBuilder.DropIndex(
                name: "IX_UserToken_AppId",
                table: "UserToken");

            migrationBuilder.DropColumn(
                name: "AppId",
                table: "UserToken");
        }
    }
}
