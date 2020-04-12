using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleERP.Document.API.Infrastructure.Migrations
{
    public partial class documentfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "DocumentInfos");

            migrationBuilder.CreateTable(
                name: "DocumentFile",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Content = table.Column<byte[]>(nullable: true),
                    ContentType = table.Column<string>(nullable: true),
                    DocumentInfoId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentFile_DocumentInfos_DocumentInfoId",
                        column: x => x.DocumentInfoId,
                        principalTable: "DocumentInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFile_DocumentInfoId",
                table: "DocumentFile",
                column: "DocumentInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentFile");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "DocumentInfos",
                nullable: true);
        }
    }
}
