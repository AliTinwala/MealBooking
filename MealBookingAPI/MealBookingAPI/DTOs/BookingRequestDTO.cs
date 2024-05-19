namespace MealBookingAPI.Application.DTOs
{
    public class BookingRequestDTO
    {
        public DateTime Booking_Date_Time { get; set; }
        public DateTime Booking_Till_Date_Time { get; set; }
        public DateTime Booking_For_Date_Time { get; set; }
    }
}
