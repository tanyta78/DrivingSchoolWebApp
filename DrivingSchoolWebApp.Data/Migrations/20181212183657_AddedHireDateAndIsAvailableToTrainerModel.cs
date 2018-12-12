using Microsoft.EntityFrameworkCore.Migrations;

namespace DrivingSchoolWebApp.Data.Migrations
{
    public partial class AddedHireDateAndIsAvailableToTrainerModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvailableLessonDay",
                table: "Trainers",
                newName: "HireDate");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Trainers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Trainers");

            migrationBuilder.RenameColumn(
                name: "HireDate",
                table: "Trainers",
                newName: "AvailableLessonDay");
        }
    }
}
