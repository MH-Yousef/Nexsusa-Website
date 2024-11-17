using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateServiceId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceItems_Services_ServiceId",
                table: "ServiceItems");

            migrationBuilder.DropColumn(
                name: "ServicesId",
                table: "ServiceItems");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "ServiceItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceItems_Services_ServiceId",
                table: "ServiceItems",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceItems_Services_ServiceId",
                table: "ServiceItems");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "ServiceItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ServicesId",
                table: "ServiceItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceItems_Services_ServiceId",
                table: "ServiceItems",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }
    }
}
