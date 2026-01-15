using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditContactMsgModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_UserId",
                schema: "Academy",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "UnitLessons",
                schema: "Academy");

            migrationBuilder.DropTable(
                name: "CourseUnits",
                schema: "Academy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                schema: "Academy",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_UserId",
                schema: "Academy",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "Academy",
                table: "ContactMessages");

            migrationBuilder.RenameTable(
                name: "Courses",
                schema: "Academy",
                newName: "Course");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Company",
                table: "WebSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Company",
                table: "WebSettings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Company",
                table: "WebSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Company",
                table: "WebSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Company",
                table: "WebSettings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Company",
                table: "Technologies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Company",
                table: "Technologies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Company",
                table: "Technologies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Company",
                table: "Technologies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Company",
                table: "Technologies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Company",
                table: "SiteContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Company",
                table: "SiteContacts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Company",
                table: "SiteContacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Company",
                table: "SiteContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Company",
                table: "SiteContacts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Company",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Company",
                table: "Services",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Company",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Company",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Company",
                table: "Services",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Company",
                table: "Missions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Company",
                table: "Missions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Company",
                table: "Missions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Company",
                table: "Missions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Company",
                table: "Missions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmploymentType",
                schema: "Academy",
                table: "Jobs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Academy",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Academy",
                table: "Jobs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Academy",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Academy",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Academy",
                table: "Jobs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Academy",
                table: "JobApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Academy",
                table: "JobApplications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Academy",
                table: "JobApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Academy",
                table: "JobApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Academy",
                table: "JobApplications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Academy",
                table: "InstructorApplications",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Academy",
                table: "InstructorApplications",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Academy",
                table: "InstructorApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Academy",
                table: "InstructorApplications",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Academy",
                table: "InstructorApplications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Company",
                table: "FlexibilitySections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Company",
                table: "FlexibilitySections",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Company",
                table: "FlexibilitySections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Company",
                table: "FlexibilitySections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Company",
                table: "FlexibilitySections",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Company",
                table: "FlexibilityItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Company",
                table: "FlexibilityItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Company",
                table: "FlexibilityItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Company",
                table: "FlexibilityItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Company",
                table: "FlexibilityItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Company",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Company",
                table: "Experiences",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Company",
                table: "Experiences",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Company",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Company",
                table: "Experiences",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Company",
                table: "ExperienceItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Company",
                table: "ExperienceItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Company",
                table: "ExperienceItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Company",
                table: "ExperienceItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Company",
                table: "ExperienceItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Company",
                table: "ExperienceCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Company",
                table: "ExperienceCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Company",
                table: "ExperienceCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Company",
                table: "ExperienceCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Company",
                table: "ExperienceCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Company",
                table: "CourseEnrollments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Company",
                table: "CourseEnrollments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Company",
                table: "CourseEnrollments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Company",
                table: "CourseEnrollments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Company",
                table: "CourseEnrollments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Academy",
                table: "ContactMessages",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Academy",
                table: "ContactMessages",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Academy",
                table: "ContactMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Academy",
                table: "ContactMessages",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Academy",
                table: "ContactMessages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Company",
                table: "CompanyJopApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Company",
                table: "CompanyJopApplications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Company",
                table: "CompanyJopApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Company",
                table: "CompanyJopApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Company",
                table: "CompanyJopApplications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Company",
                table: "CompanyCourses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Company",
                table: "CompanyCourses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Company",
                table: "CompanyCourses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Company",
                table: "CompanyCourses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Company",
                table: "CompanyCourses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Company",
                table: "CompanyContactMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Company",
                table: "CompanyContactMessages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Company",
                table: "CompanyContactMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Company",
                table: "CompanyContactMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Company",
                table: "CompanyContactMessages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Company",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Company",
                table: "Clients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Company",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Company",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Company",
                table: "Clients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Company",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Company",
                table: "Articles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Company",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Company",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Company",
                table: "Articles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Company",
                table: "Achievements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Company",
                table: "Achievements",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Company",
                table: "Achievements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Company",
                table: "Achievements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Company",
                table: "Achievements",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Company",
                table: "AboutUs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Company",
                table: "AboutUs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Company",
                table: "AboutUs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "Company",
                table: "AboutUs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Company",
                table: "AboutUs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Course",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Course",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Course",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Course",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "Course",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                table: "Course",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Company",
                table: "WebSettings");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Company",
                table: "WebSettings");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Company",
                table: "WebSettings");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Company",
                table: "WebSettings");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Company",
                table: "WebSettings");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Company",
                table: "Technologies");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Company",
                table: "Technologies");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Company",
                table: "Technologies");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Company",
                table: "Technologies");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Company",
                table: "Technologies");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Company",
                table: "SiteContacts");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Company",
                table: "SiteContacts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Company",
                table: "SiteContacts");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Company",
                table: "SiteContacts");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Company",
                table: "SiteContacts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Company",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Company",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Company",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Company",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Company",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Company",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Company",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Company",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Company",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Company",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Academy",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Academy",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Academy",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Academy",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Academy",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Academy",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Academy",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Academy",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Academy",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Academy",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Academy",
                table: "InstructorApplications");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Academy",
                table: "InstructorApplications");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Academy",
                table: "InstructorApplications");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Academy",
                table: "InstructorApplications");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Academy",
                table: "InstructorApplications");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Company",
                table: "FlexibilitySections");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Company",
                table: "FlexibilitySections");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Company",
                table: "FlexibilitySections");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Company",
                table: "FlexibilitySections");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Company",
                table: "FlexibilitySections");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Company",
                table: "FlexibilityItems");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Company",
                table: "FlexibilityItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Company",
                table: "FlexibilityItems");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Company",
                table: "FlexibilityItems");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Company",
                table: "FlexibilityItems");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Company",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Company",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Company",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Company",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Company",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Company",
                table: "ExperienceItems");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Company",
                table: "ExperienceItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Company",
                table: "ExperienceItems");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Company",
                table: "ExperienceItems");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Company",
                table: "ExperienceItems");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Company",
                table: "ExperienceCategories");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Company",
                table: "ExperienceCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Company",
                table: "ExperienceCategories");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Company",
                table: "ExperienceCategories");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Company",
                table: "ExperienceCategories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Company",
                table: "CourseEnrollments");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Company",
                table: "CourseEnrollments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Company",
                table: "CourseEnrollments");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Company",
                table: "CourseEnrollments");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Company",
                table: "CourseEnrollments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Academy",
                table: "ContactMessages");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Academy",
                table: "ContactMessages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Academy",
                table: "ContactMessages");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Academy",
                table: "ContactMessages");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Academy",
                table: "ContactMessages");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Company",
                table: "CompanyJopApplications");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Company",
                table: "CompanyJopApplications");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Company",
                table: "CompanyJopApplications");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Company",
                table: "CompanyJopApplications");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Company",
                table: "CompanyJopApplications");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Company",
                table: "CompanyCourses");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Company",
                table: "CompanyCourses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Company",
                table: "CompanyCourses");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Company",
                table: "CompanyCourses");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Company",
                table: "CompanyCourses");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Company",
                table: "CompanyContactMessages");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Company",
                table: "CompanyContactMessages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Company",
                table: "CompanyContactMessages");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Company",
                table: "CompanyContactMessages");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Company",
                table: "CompanyContactMessages");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Company",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Company",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Company",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Company",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Company",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Company",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Company",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Company",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Company",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Company",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Company",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Company",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Company",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Company",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Company",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Company",
                table: "AboutUs");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Company",
                table: "AboutUs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Company",
                table: "AboutUs");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Company",
                table: "AboutUs");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Company",
                table: "AboutUs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "Course");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "Courses",
                newSchema: "Academy");

            migrationBuilder.AlterColumn<string>(
                name: "EmploymentType",
                schema: "Academy",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "Academy",
                table: "ContactMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "Academy",
                table: "Courses",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "Academy",
                table: "Courses",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Academy",
                table: "Courses",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                schema: "Academy",
                table: "Courses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CourseUnits",
                schema: "Academy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseUnits_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "Academy",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitLessons",
                schema: "Academy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseUnitId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitLessons_CourseUnits_CourseUnitId",
                        column: x => x.CourseUnitId,
                        principalSchema: "Academy",
                        principalTable: "CourseUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_UserId",
                schema: "Academy",
                table: "Courses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUnits_CourseId",
                schema: "Academy",
                table: "CourseUnits",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitLessons_CourseUnitId",
                schema: "Academy",
                table: "UnitLessons",
                column: "CourseUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_UserId",
                schema: "Academy",
                table: "Courses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
