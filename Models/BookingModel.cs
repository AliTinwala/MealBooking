using System.ComponentModel.DataAnnotations;

namespace MEAL_2024_API.Models
{
    public class BookingModel
    {
        [Key]
        public Guid BookingId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        //public Guid CouponId { get; set; }
        public string MealType {  get; set; }
        public DateTime BookingDate {get; set; }
        public DateTime CreatedDate{ get; set; }
        public bool IsCancelled { get; set; }

    }
}
