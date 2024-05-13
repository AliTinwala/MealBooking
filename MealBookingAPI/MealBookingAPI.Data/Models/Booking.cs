using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealBookingAPI.Data.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int Calendar_Id { get; set; }
        public int Coupon_Id { get; set; }
        public DateTime Booking_Date_Time { get; set; }
        public DateTime Booking_Till_Date_Time { get; set; }
        public DateTime Booking_For_Date_Time { get; set; }
    }
}
