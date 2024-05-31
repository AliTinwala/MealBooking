using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealBookingAPI.Data.Migrations
{
    public partial class UpdateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Notification",
                newName: "NotificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NotificationId",
                table: "Notification",
                newName: "Id");
        }
    }
}
