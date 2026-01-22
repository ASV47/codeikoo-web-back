using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditArAndEnLocalization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Description_Ar",
                schema: "Academy",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Description_En",
                schema: "Academy",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "Title_En",
                schema: "Academy",
                table: "UnitLessons",
                newName: "TitleEnglish");

            migrationBuilder.RenameColumn(
                name: "Title_Ar",
                schema: "Academy",
                table: "UnitLessons",
                newName: "TilteArabic");

            migrationBuilder.RenameColumn(
                name: "Title_En",
                schema: "Academy",
                table: "Jobs",
                newName: "TitleEnglish");

            migrationBuilder.RenameColumn(
                name: "Title_Ar",
                schema: "Academy",
                table: "Jobs",
                newName: "TilteArabic");

            migrationBuilder.RenameColumn(
                name: "Requirements_En",
                schema: "Academy",
                table: "Jobs",
                newName: "RequirementsEn");

            migrationBuilder.RenameColumn(
                name: "Requirements_Ar",
                schema: "Academy",
                table: "Jobs",
                newName: "RequirementsAr");

            migrationBuilder.RenameColumn(
                name: "Location_En",
                schema: "Academy",
                table: "Jobs",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "Title_En",
                schema: "Academy",
                table: "CourseUnits",
                newName: "TitleEnglish");

            migrationBuilder.RenameColumn(
                name: "Title_Ar",
                schema: "Academy",
                table: "CourseUnits",
                newName: "TilteArabic");

            migrationBuilder.RenameColumn(
                name: "Title_En",
                schema: "Academy",
                table: "Courses",
                newName: "TitleEnglish");

            migrationBuilder.RenameColumn(
                name: "Title_Ar",
                schema: "Academy",
                table: "Courses",
                newName: "TilteArabic");

            migrationBuilder.RenameColumn(
                name: "Features_En",
                schema: "Academy",
                table: "Courses",
                newName: "FeaturesEn");

            migrationBuilder.RenameColumn(
                name: "Features_Ar",
                schema: "Academy",
                table: "Courses",
                newName: "FeaturesAr");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAr",
                schema: "Academy",
                table: "UnitLessons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                schema: "Academy",
                table: "UnitLessons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAr",
                schema: "Academy",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                schema: "Academy",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAr",
                schema: "Academy",
                table: "CourseUnits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                schema: "Academy",
                table: "CourseUnits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAr",
                schema: "Academy",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                schema: "Academy",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionAr",
                schema: "Academy",
                table: "UnitLessons");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                schema: "Academy",
                table: "UnitLessons");

            migrationBuilder.DropColumn(
                name: "DescriptionAr",
                schema: "Academy",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                schema: "Academy",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "DescriptionAr",
                schema: "Academy",
                table: "CourseUnits");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                schema: "Academy",
                table: "CourseUnits");

            migrationBuilder.DropColumn(
                name: "DescriptionAr",
                schema: "Academy",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                schema: "Academy",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "TitleEnglish",
                schema: "Academy",
                table: "UnitLessons",
                newName: "Title_En");

            migrationBuilder.RenameColumn(
                name: "TilteArabic",
                schema: "Academy",
                table: "UnitLessons",
                newName: "Title_Ar");

            migrationBuilder.RenameColumn(
                name: "TitleEnglish",
                schema: "Academy",
                table: "Jobs",
                newName: "Title_En");

            migrationBuilder.RenameColumn(
                name: "TilteArabic",
                schema: "Academy",
                table: "Jobs",
                newName: "Title_Ar");

            migrationBuilder.RenameColumn(
                name: "RequirementsEn",
                schema: "Academy",
                table: "Jobs",
                newName: "Requirements_En");

            migrationBuilder.RenameColumn(
                name: "RequirementsAr",
                schema: "Academy",
                table: "Jobs",
                newName: "Requirements_Ar");

            migrationBuilder.RenameColumn(
                name: "Location",
                schema: "Academy",
                table: "Jobs",
                newName: "Location_En");

            migrationBuilder.RenameColumn(
                name: "TitleEnglish",
                schema: "Academy",
                table: "CourseUnits",
                newName: "Title_En");

            migrationBuilder.RenameColumn(
                name: "TilteArabic",
                schema: "Academy",
                table: "CourseUnits",
                newName: "Title_Ar");

            migrationBuilder.RenameColumn(
                name: "TitleEnglish",
                schema: "Academy",
                table: "Courses",
                newName: "Title_En");

            migrationBuilder.RenameColumn(
                name: "TilteArabic",
                schema: "Academy",
                table: "Courses",
                newName: "Title_Ar");

            migrationBuilder.RenameColumn(
                name: "FeaturesEn",
                schema: "Academy",
                table: "Courses",
                newName: "Features_En");

            migrationBuilder.RenameColumn(
                name: "FeaturesAr",
                schema: "Academy",
                table: "Courses",
                newName: "Features_Ar");

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
                name: "Description_Ar",
                schema: "Academy",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description_En",
                schema: "Academy",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
