using MealBookingAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MealBookingAPI.Application.Services.IServices;

namespace MealBookingAPI.Application.Services
{
    public class BookingServices: IBookingServices
    {
        private readonly AppDbContext _dbContext;

        public BookingServices(AppDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet("exists/{date}")]
        public async Task<ActionResult<bool>> BookingExists(DateTime date)
        {
            var exists = await _dbContext.Booking.AnyAsync(b => b.Booking_For_Date_Time.Date == date.Date);
            return exists;
        }
    }
}
