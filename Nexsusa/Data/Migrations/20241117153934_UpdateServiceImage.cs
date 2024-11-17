using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateServiceImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_ServiceItems_ServiceItemId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ServiceItemId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ServiceItemId",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "Images1",
                table: "ServiceItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Images2",
                table: "ServiceItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Images3",
                table: "ServiceItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ServicePages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicePages_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServicePages_ServiceId",
                table: "ServicePages",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServicePages");

            migrationBuilder.DropColumn(
                name: "Images1",
                table: "ServiceItems");

            migrationBuilder.DropColumn(
                name: "Images2",
                table: "ServiceItems");

            migrationBuilder.DropColumn(
                name: "Images3",
                table: "ServiceItems");

            migrationBuilder.AddColumn<int>(
                name: "ServiceItemId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ServiceItemId",
                table: "Images",
                column: "ServiceItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ServiceItems_ServiceItemId",
                table: "Images",
                column: "ServiceItemId",
                principalTable: "ServiceItems",
                principalColumn: "Id");
        }
    }
}
