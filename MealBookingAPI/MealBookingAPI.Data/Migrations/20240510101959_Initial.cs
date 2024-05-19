using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealBookingAPI.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    Calendar_Id = table.Column<int>(type: "int", nullable: false),
                    Coupon_Id = table.Column<int>(type: "int", nullable: false),
                    Booking_Date_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Booking_Till_Date_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Booking_For_Date_Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
