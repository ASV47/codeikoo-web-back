using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddModelsInAcademySchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitLesson",
                table: "UnitLesson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseUnit",
                table: "CourseUnit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                table: "Course");

            migrationBuilder.RenameTable(
                name: "UnitLesson",
                newName: "UnitLessons",
                newSchema: "Academy");

            migrationBuilder.RenameTable(
                name: "CourseUnit",
                newName: "CourseUnits",
                newSchema: "Academy");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "Courses",
                newSchema: "Academy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitLessons",
                schema: "Academy",
                table: "UnitLessons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseUnits",
                schema: "Academy",
                table: "CourseUnits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                schema: "Academy",
                table: "Courses",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitLessons",
                schema: "Academy",
                table: "UnitLessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseUnits",
                schema: "Academy",
                table: "CourseUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                schema: "Academy",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "UnitLessons",
                schema: "Academy",
                newName: "UnitLesson");

            migrationBuilder.RenameTable(
                name: "CourseUnits",
                schema: "Academy",
                newName: "CourseUnit");

            migrationBuilder.RenameTable(
                name: "Courses",
                schema: "Academy",
                newName: "Course");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitLesson",
                table: "UnitLesson",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseUnit",
                table: "CourseUnit",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                table: "Course",
                column: "Id");
        }
    }
}
