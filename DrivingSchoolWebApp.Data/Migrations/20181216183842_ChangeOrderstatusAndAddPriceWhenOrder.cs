using Microsoft.EntityFrameworkCore.Migrations;

namespace DrivingSchoolWebApp.Data.Migrations
{
    public partial class ChangeOrderstatusAndAddPriceWhenOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ActualPriceWhenOrder",
                table: "Orders",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualPriceWhenOrder",
                table: "Orders");
        }
    }
}
