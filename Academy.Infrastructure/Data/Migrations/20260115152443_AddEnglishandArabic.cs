using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEnglishandArabic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Course",
                newName: "Title_En");

            migrationBuilder.RenameColumn(
                name: "Features",
                table: "Course",
                newName: "Title_Ar");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Course",
                newName: "Features_En");

            migrationBuilder.AddColumn<string>(
                name: "Description_Ar",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description_En",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Features_Ar",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description_Ar",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Description_En",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Features_Ar",
                table: "Course");

            migrationBuilder.RenameColumn(
                name: "Title_En",
                table: "Course",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Title_Ar",
                table: "Course",
                newName: "Features");

            migrationBuilder.RenameColumn(
                name: "Features_En",
                table: "Course",
                newName: "Description");
        }
    }
}
