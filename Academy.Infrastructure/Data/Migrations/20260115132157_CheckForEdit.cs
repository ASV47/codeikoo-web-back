using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CheckForEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                schema: "Academy",
                table: "Jobs",
                newName: "Title_En");

            migrationBuilder.RenameColumn(
                name: "Requirements",
                schema: "Academy",
                table: "Jobs",
                newName: "Title_Ar");

            migrationBuilder.RenameColumn(
                name: "Location",
                schema: "Academy",
                table: "Jobs",
                newName: "Requirements_En");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "Academy",
                table: "Jobs",
                newName: "Requirements_Ar");

            migrationBuilder.AddColumn<string>(
                name: "Description_Ar",
                schema: "Academy",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description_En",
                schema: "Academy",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location_Ar",
                schema: "Academy",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location_En",
                schema: "Academy",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description_Ar",
                schema: "Academy",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Description_En",
                schema: "Academy",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Location_Ar",
                schema: "Academy",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Location_En",
                schema: "Academy",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "Title_En",
                schema: "Academy",
                table: "Jobs",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Title_Ar",
                schema: "Academy",
                table: "Jobs",
                newName: "Requirements");

            migrationBuilder.RenameColumn(
                name: "Requirements_En",
                schema: "Academy",
                table: "Jobs",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "Requirements_Ar",
                schema: "Academy",
                table: "Jobs",
                newName: "Description");
        }
    }
}
