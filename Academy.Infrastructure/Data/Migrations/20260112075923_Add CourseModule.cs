using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstructorApplications_Users_UserId",
                table: "InstructorApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_Users_UserId",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_UserId",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_InstructorApplications_UserId",
                table: "InstructorApplications");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "InstructorApplications");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "ContactMessages",
                newName: "PhoneNumber");

            migrationBuilder.AddColumn<string>(
                name: "Requirements",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    CourseImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Features = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseUnits_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitLessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CourseUnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitLessons_CourseUnits_CourseUnitId",
                        column: x => x.CourseUnitId,
                        principalTable: "CourseUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_UserId",
                table: "Courses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUnits_CourseId",
                table: "CourseUnits",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitLessons_CourseUnitId",
                table: "UnitLessons",
                column: "CourseUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitLessons");

            migrationBuilder.DropTable(
                name: "CourseUnits");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropColumn(
                name: "Requirements",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "ContactMessages",
                newName: "Email");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "JobApplications",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "InstructorApplications",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_UserId",
                table: "JobApplications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorApplications_UserId",
                table: "InstructorApplications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorApplications_Users_UserId",
                table: "InstructorApplications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_Users_UserId",
                table: "JobApplications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
