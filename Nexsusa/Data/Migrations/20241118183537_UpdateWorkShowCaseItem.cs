using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkShowCaseItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkShowCaseServices");

            migrationBuilder.DropTable(
                name: "WorkShowCaseNavBarItems");

            migrationBuilder.DropTable(
                name: "WorkShowCaseNavBars");

            migrationBuilder.CreateTable(
                name: "WorkShowCaseItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkShowCaseId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShowCaseItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkShowCaseItem_WorkShowCases_WorkShowCaseId",
                        column: x => x.WorkShowCaseId,
                        principalTable: "WorkShowCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkShowCaseItem_WorkShowCaseId",
                table: "WorkShowCaseItem",
                column: "WorkShowCaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkShowCaseItem");

            migrationBuilder.CreateTable(
                name: "WorkShowCaseNavBars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkShowCaseId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HasSubItem = table.Column<bool>(type: "bit", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShowCaseNavBars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkShowCaseNavBars_WorkShowCases_WorkShowCaseId",
                        column: x => x.WorkShowCaseId,
                        principalTable: "WorkShowCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkShowCaseNavBarItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkShowCaseNavBarId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShowCaseNavBarItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkShowCaseNavBarItems_WorkShowCaseNavBars_WorkShowCaseNavBarId",
                        column: x => x.WorkShowCaseNavBarId,
                        principalTable: "WorkShowCaseNavBars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkShowCaseServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkShowCaseNavBarItemId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SubDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShowCaseServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkShowCaseServices_WorkShowCaseNavBarItems_WorkShowCaseNavBarItemId",
                        column: x => x.WorkShowCaseNavBarItemId,
                        principalTable: "WorkShowCaseNavBarItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkShowCaseNavBarItems_WorkShowCaseNavBarId",
                table: "WorkShowCaseNavBarItems",
                column: "WorkShowCaseNavBarId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShowCaseNavBars_WorkShowCaseId",
                table: "WorkShowCaseNavBars",
                column: "WorkShowCaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkShowCaseServices_WorkShowCaseNavBarItemId",
                table: "WorkShowCaseServices",
                column: "WorkShowCaseNavBarItemId");
        }
    }
}
