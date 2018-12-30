using Microsoft.EntityFrameworkCore.Migrations;

namespace DrivingSchoolWebApp.Data.Migrations
{
    public partial class UpdateSchoolModelRemoveOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Schools_SchoolId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SchoolId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SchoolId",
                table: "Orders",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Schools_SchoolId",
                table: "Orders",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
