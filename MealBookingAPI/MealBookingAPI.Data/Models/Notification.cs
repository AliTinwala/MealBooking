using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealBookingAPI.Data.Models
{
    public  class Notification
    {
        [Key]
        public Guid NotificationId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string ?Message { get; set; }
        public bool isRead { get; set; }

    }
}
