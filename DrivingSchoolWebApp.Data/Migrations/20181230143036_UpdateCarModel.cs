using Microsoft.EntityFrameworkCore.Migrations;

namespace DrivingSchoolWebApp.Data.Migrations
{
    public partial class UpdateCarModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Model",
                table: "Cars",
                newName: "CarModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarModel",
                table: "Cars",
                newName: "Model");
        }
    }
}
