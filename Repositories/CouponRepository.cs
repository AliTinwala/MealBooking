using MEAL_2024_API.Context;
using MEAL_2024_API.Models;
using MEAL_2024_API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace MEAL_2024_API.Repositories
{
    public class CouponRepository : Repository<CouponModel>, ICouponRepository
    {
       
        public CouponRepository(AppDbContext dbContext) : base(dbContext)
        {
           
        }

        public async Task<bool> CouponExistsAsync(Guid bookingId)
        {
            return await DbSet.AnyAsync(c => c.BookingId == bookingId);
        }

        public async Task<CouponModel> GetByBookingIdAsync(Guid bookingId)
        {
            return await DbSet.FirstOrDefaultAsync(c => c.BookingId == bookingId);
        }
    }
}
