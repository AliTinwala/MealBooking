namespace MealBookingAPI.Application.DTOs
{
    public class BookingRequestDTO
    {
        public string Booking_Type { get; set; }
        public DateTime Booking_Date_Time { get; set; }
        public DateTime Booking_For_Date_Time { get; set; }
    }
}
