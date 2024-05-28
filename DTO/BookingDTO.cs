namespace MEAL_2024_API.DTO
{
    public class BookingDTO
    {
         public Guid BookingId { get; set; }
        public Guid UserId { get; set; }
        public string MealType { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsCancelled { get; set; }
    }
}
