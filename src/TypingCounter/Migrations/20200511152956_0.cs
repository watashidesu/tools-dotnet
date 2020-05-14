using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TypingCounter.Migrations
{
    public partial class _0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArchiveDate",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchiveDate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Current",
                columns: table => new
                {
                    Code = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(nullable: false),
                    Count = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Current", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Archive",
                columns: table => new
                {
                    ArchiveId = table.Column<long>(nullable: false),
                    Code = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(nullable: false),
                    Count = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archive", x => new { x.ArchiveId, x.Code });
                    table.ForeignKey(
                        name: "FK_Archive_ArchiveDate_ArchiveId",
                        column: x => x.ArchiveId,
                        principalTable: "ArchiveDate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Archive");

            migrationBuilder.DropTable(
                name: "Current");

            migrationBuilder.DropTable(
                name: "ArchiveDate");
        }
    }
}
