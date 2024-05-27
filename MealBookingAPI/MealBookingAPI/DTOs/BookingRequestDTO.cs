namespace MealBookingAPI.Application.DTOs
{
    public class BookingRequestDTO
    {
        public string MealType { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime BookingForDate { get; set; }
    }
}
