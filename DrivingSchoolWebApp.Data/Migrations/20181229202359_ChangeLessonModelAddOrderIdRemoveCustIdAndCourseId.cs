using Microsoft.EntityFrameworkCore.Migrations;

namespace DrivingSchoolWebApp.Data.Migrations
{
    public partial class ChangeLessonModelAddOrderIdRemoveCustIdAndCourseId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Courses_CourseId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Customers_CustomerId",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Lessons",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_CourseId",
                table: "Lessons",
                newName: "IX_Lessons_OrderId");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Lessons",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Customers_CustomerId",
                table: "Lessons",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Orders_OrderId",
                table: "Lessons",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Customers_CustomerId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Orders_OrderId",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Lessons",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_OrderId",
                table: "Lessons",
                newName: "IX_Lessons_CourseId");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Lessons",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Courses_CourseId",
                table: "Lessons",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Customers_CustomerId",
                table: "Lessons",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
