using MEAL_2024_API.Models;

namespace MEAL_2024_API.Repositories.Interface
{
    public interface IBookingRepository:IRepository<BookingModel>
    {
        Task<IEnumerable<BookingModel>> GetBookingsByUserIdAsync(Guid userId, DateTime? date=null);
        Task<IEnumerable<BookingModel>> GetBookingsByDateRangeAsync(DateTime startDate, DateTime endDate);

        Task<BookingModel> GetBookingsByUserIdAndMealAsync(Guid userId, DateTime date, string mealType);
    }
}
