using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealBookingAPI.Data.Migrations
{
    public partial class UpdateDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BookingType",
                table: "Booking",
                newName: "Booking_Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Booking_Type",
                table: "Booking",
                newName: "BookingType");
        }
    }
}
