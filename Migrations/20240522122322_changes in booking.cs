using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MEAL_2024_API.Migrations
{
    public partial class changesinbooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "BookingForDate",
                table: "Bookings",
                newName: "CreatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Bookings",
                newName: "BookingForDate");

            migrationBuilder.AddColumn<Guid>(
                name: "CouponId",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
