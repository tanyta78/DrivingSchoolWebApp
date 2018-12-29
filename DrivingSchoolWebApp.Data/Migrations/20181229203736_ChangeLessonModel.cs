using Microsoft.EntityFrameworkCore.Migrations;

namespace DrivingSchoolWebApp.Data.Migrations
{
    public partial class ChangeLessonModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Customers_CustomerId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_CustomerId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Lessons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Lessons",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CustomerId",
                table: "Lessons",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Customers_CustomerId",
                table: "Lessons",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
