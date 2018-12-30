using Microsoft.EntityFrameworkCore.Migrations;

namespace DrivingSchoolWebApp.Data.Migrations
{
    public partial class UpdateCourseModelRemoveRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Courses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Courses",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
