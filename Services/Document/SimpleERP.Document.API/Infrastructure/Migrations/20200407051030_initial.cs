using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleERP.Document.API.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Readonly = table.Column<bool>(nullable: false),
                    Hidden = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Issuers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Readonly = table.Column<bool>(nullable: false),
                    Hidden = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issuers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Readonly = table.Column<bool>(nullable: false),
                    Hidden = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentInfos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    No = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    DateOfRelease = table.Column<string>(nullable: true),
                    DateOfCreate = table.Column<string>(nullable: true),
                    Creator = table.Column<string>(nullable: true),
                    DateOfModify = table.Column<string>(nullable: true),
                    Modifier = table.Column<string>(nullable: true),
                    IssuerId = table.Column<long>(nullable: false),
                    DomainId = table.Column<long>(nullable: false),
                    TypeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentInfos_Domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentInfos_Issuers_IssuerId",
                        column: x => x.IssuerId,
                        principalTable: "Issuers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentInfos_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Domains",
                columns: new[] { "Id", "Hidden", "Readonly", "Title" },
                values: new object[,]
                {
                    { 1L, false, true, "اداری" },
                    { 2L, false, true, "مالی" },
                    { 3L, false, true, "فناوری اطلاعات" },
                    { 4L, false, true, "فنی" }
                });

            migrationBuilder.InsertData(
                table: "Issuers",
                columns: new[] { "Id", "Hidden", "Readonly", "Title" },
                values: new object[,]
                {
                    { 1L, false, true, "سازمان برنامه و بودجه" },
                    { 2L, false, true, "سازمان نظام مهندسی" },
                    { 3L, false, true, "وزارت راه و شهرسازی" },
                    { 4L, false, true, "سازمان اداری و استخدامی" },
                    { 5L, false, true, "وزارت اقتصاد و دارایی" },
                    { 6L, false, true, "هیأت دولت" },
                    { 7L, false, true, "قانون مصوب" }
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Id", "Hidden", "Readonly", "Title" },
                values: new object[,]
                {
                    { 1L, false, true, "آئین‌نامه" },
                    { 2L, false, true, "بخشنامه" },
                    { 3L, false, true, "دستورالعمل" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentInfos_DomainId",
                table: "DocumentInfos",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentInfos_IssuerId",
                table: "DocumentInfos",
                column: "IssuerId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentInfos_TypeId",
                table: "DocumentInfos",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentInfos");

            migrationBuilder.DropTable(
                name: "Domains");

            migrationBuilder.DropTable(
                name: "Issuers");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
