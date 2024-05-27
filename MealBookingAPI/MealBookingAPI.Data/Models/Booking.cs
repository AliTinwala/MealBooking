using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MealBookingAPI.Data.Models
{
    public class Booking
    {
        [Key]
        public Guid BookingId { get; set; } 
        public Guid UserId { get; set; }
        public User User { get; set; }
        [Required, DefaultValue(0)]
        public Guid CouponId { get; set; }
        [Required]
        public string MealType { get; set; }
        [Required]
        public DateTime BookingDate { get; set; }
        [Required]
        public DateTime BookingForDate { get; set; }
        public bool isCancelled { get; set; }
    }
}
