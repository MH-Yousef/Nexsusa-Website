using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkShowCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "WorkShowCaseNavBarItems");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "WorkShowCaseNavBarItems");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "WorkShowCaseNavBarItems");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "WorkShowCaseServices",
                newName: "SubTitle");

            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "WorkShowCaseServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "WorkShowCaseServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image3",
                table: "WorkShowCaseServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubDescription",
                table: "WorkShowCaseServices",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image1",
                table: "WorkShowCaseServices");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "WorkShowCaseServices");

            migrationBuilder.DropColumn(
                name: "Image3",
                table: "WorkShowCaseServices");

            migrationBuilder.DropColumn(
                name: "SubDescription",
                table: "WorkShowCaseServices");

            migrationBuilder.RenameColumn(
                name: "SubTitle",
                table: "WorkShowCaseServices",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "WorkShowCaseNavBarItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "WorkShowCaseNavBarItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "WorkShowCaseNavBarItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
