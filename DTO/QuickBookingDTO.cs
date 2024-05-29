using System.ComponentModel.DataAnnotations;

namespace MEAL_2024_API.DTO
{
    public class QuickBookingDTO
    {
        public Guid UserId { get; set; }
        public string MealType { get; set; } // Lunch or Dinner
        [Required]
        public DateTime Date { get; set; }
    }
}
