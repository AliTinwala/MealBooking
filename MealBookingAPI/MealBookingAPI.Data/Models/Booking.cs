using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealBookingAPI.Data.Models
{
    public class Booking
    {
        [Required, DefaultValue(0)]
        public int Id { get; set; }
        [Required, DefaultValue(0)]
        public int User_Id { get; set; }
        [Required, DefaultValue(0)]
        public int Coupon_Id { get; set; }
        [Required]
        public string Booking_Type { get; set; }
        [Required]
        public DateTime Booking_Date_Time { get; set; }
        [Required]
        public DateTime Booking_For_Date_Time { get; set; }
    }
}
