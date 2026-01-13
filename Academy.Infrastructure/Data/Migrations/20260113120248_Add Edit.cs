using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Company");

            migrationBuilder.EnsureSchema(
                name: "Academy");

            migrationBuilder.RenameTable(
                name: "UnitLessons",
                newName: "UnitLessons",
                newSchema: "Academy");

            migrationBuilder.RenameTable(
                name: "Jobs",
                newName: "Jobs",
                newSchema: "Academy");

            migrationBuilder.RenameTable(
                name: "JobApplications",
                newName: "JobApplications",
                newSchema: "Academy");

            migrationBuilder.RenameTable(
                name: "InstructorApplications",
                newName: "InstructorApplications",
                newSchema: "Academy");

            migrationBuilder.RenameTable(
                name: "CourseUnits",
                newName: "CourseUnits",
                newSchema: "Academy");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Courses",
                newSchema: "Academy");

            migrationBuilder.RenameTable(
                name: "ContactMessages",
                newName: "ContactMessages",
                newSchema: "Academy");

            migrationBuilder.CreateTable(
                name: "AboutUs",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyContactMessages",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    termsAccepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyContactMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyCourses",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCourses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyJopApplications",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    termsAccepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyJopApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceCategories",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlexibilityItems",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlexibilityItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlexibilitySections",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlexibilitySections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Missions",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteContacts",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteContacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Technologies",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechnologyUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technologies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebSettings",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientJop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseEnrollments",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEnrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseEnrollments_CompanyCourses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "Company",
                        principalTable: "CompanyCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceItems",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperienceCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperienceItems_ExperienceCategories_ExperienceCategoryId",
                        column: x => x.ExperienceCategoryId,
                        principalSchema: "Company",
                        principalTable: "ExperienceCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrollments_CourseId",
                schema: "Company",
                table: "CourseEnrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceItems_ExperienceCategoryId",
                schema: "Company",
                table: "ExperienceItems",
                column: "ExperienceCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutUs",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "Achievements",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "Articles",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "CompanyContactMessages",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "CompanyJopApplications",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "CourseEnrollments",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "ExperienceItems",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "Experiences",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "FlexibilityItems",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "FlexibilitySections",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "Missions",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "Services",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "SiteContacts",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "Technologies",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "WebSettings",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "CompanyCourses",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "ExperienceCategories",
                schema: "Company");

            migrationBuilder.RenameTable(
                name: "UnitLessons",
                schema: "Academy",
                newName: "UnitLessons");

            migrationBuilder.RenameTable(
                name: "Jobs",
                schema: "Academy",
                newName: "Jobs");

            migrationBuilder.RenameTable(
                name: "JobApplications",
                schema: "Academy",
                newName: "JobApplications");

            migrationBuilder.RenameTable(
                name: "InstructorApplications",
                schema: "Academy",
                newName: "InstructorApplications");

            migrationBuilder.RenameTable(
                name: "CourseUnits",
                schema: "Academy",
                newName: "CourseUnits");

            migrationBuilder.RenameTable(
                name: "Courses",
                schema: "Academy",
                newName: "Courses");

            migrationBuilder.RenameTable(
                name: "ContactMessages",
                schema: "Academy",
                newName: "ContactMessages");
        }
    }
}
