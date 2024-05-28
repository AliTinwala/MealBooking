using MEAL_2024_API.Context;
using MEAL_2024_API.Models;
using MEAL_2024_API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace MEAL_2024_API.Repositories
{
    public class BookingRepository : Repository<BookingModel>, IBookingRepository
    {
        private readonly AppDbContext _dbContext;
        public BookingRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BookingModel>> GetBookingsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbContext.Bookings.Where(b => b.BookingDate >= startDate && b.BookingDate <= endDate).ToListAsync();
        }

        public async Task<BookingModel> GetBookingsByUserIdAndMealAsync(Guid userId, DateTime date, string mealType)
        {
            return await _dbContext.Bookings
        .Where(b => b.UserId == userId && b.BookingDate.Date == date.Date && b.MealType == mealType && !b.IsCancelled)
        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BookingModel>> GetBookingsByUserIdAsync(Guid userId, DateTime? date)
        {


            if (date.HasValue)
            {
                return await _dbContext.Bookings
                    .Where(b => b.UserId == userId && b.BookingDate.Date == date.Value.Date)
                    .ToListAsync();
            }
            else
            {
                return await _dbContext.Bookings
                    .Where(b => b.UserId == userId)
                    .ToListAsync();
            }
        }
    }
}
