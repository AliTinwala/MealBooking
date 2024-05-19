namespace MealBookingAPI.Dtos
{
    public class BookingDTO
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
