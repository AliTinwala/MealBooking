using MEAL_2024_API.DTO;

namespace MEAL_2024_API.Services.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDTO>> GetBookingsByUserIdAsync(Guid userId);
        Task<IEnumerable<BookingDTO>> GetBookingsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task CreateBookingAsync(BookingCreateDTO bookingCreateDTO);
        Task UpdateBookingAsync(Guid bookingId, BookingDTO bookingDTO);
        Task DeleteBookingAsync(Guid userId, DateTime bookingDate, string mealType);
    }
}
