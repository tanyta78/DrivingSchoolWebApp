using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DrivingSchoolWebApp.Data.Migrations
{
    public partial class LessonModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfLesson",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Lessons");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Lessons",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Lessons",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFullDay",
                table: "Lessons",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ThemeColor",
                table: "Lessons",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "IsFullDay",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "ThemeColor",
                table: "Lessons");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfLesson",
                table: "Lessons",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Lessons",
                nullable: false,
                defaultValue: 0);
        }
    }
}
