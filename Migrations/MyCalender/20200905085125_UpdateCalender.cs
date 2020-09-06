using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HomeAutomation.Migrations.MyCalender
{
    public partial class UpdateCalender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CalenderId",
                table: "Calendar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Calendar",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Calendar",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UploadFileTypeId",
                table: "Calendar",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: false),
                    CalenderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsoCode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadFileType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    HasAutoUpdate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadFileType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalenderCategrory",
                columns: table => new
                {
                    CalenderId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    CategoryId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalenderCategrory", x => new { x.CalenderId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_CalenderCategrory_Calendar_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Calendar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalenderCategrory_Category_CategoryId1",
                        column: x => x.CategoryId1,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_CalenderId",
                table: "Calendar",
                column: "CalenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_LanguageId",
                table: "Calendar",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_UploadFileTypeId",
                table: "Calendar",
                column: "UploadFileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CalenderCategrory_CategoryId",
                table: "CalenderCategrory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CalenderCategrory_CategoryId1",
                table: "CalenderCategrory",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentId",
                table: "Category",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendar_Category_CalenderId",
                table: "Calendar",
                column: "CalenderId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Calendar_Language_LanguageId",
                table: "Calendar",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Calendar_UploadFileType_UploadFileTypeId",
                table: "Calendar",
                column: "UploadFileTypeId",
                principalTable: "UploadFileType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendar_Category_CalenderId",
                table: "Calendar");

            migrationBuilder.DropForeignKey(
                name: "FK_Calendar_Language_LanguageId",
                table: "Calendar");

            migrationBuilder.DropForeignKey(
                name: "FK_Calendar_UploadFileType_UploadFileTypeId",
                table: "Calendar");

            migrationBuilder.DropTable(
                name: "CalenderCategrory");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "UploadFileType");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Calendar_CalenderId",
                table: "Calendar");

            migrationBuilder.DropIndex(
                name: "IX_Calendar_LanguageId",
                table: "Calendar");

            migrationBuilder.DropIndex(
                name: "IX_Calendar_UploadFileTypeId",
                table: "Calendar");

            migrationBuilder.DropColumn(
                name: "CalenderId",
                table: "Calendar");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Calendar");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Calendar");

            migrationBuilder.DropColumn(
                name: "UploadFileTypeId",
                table: "Calendar");
        }
    }
}
