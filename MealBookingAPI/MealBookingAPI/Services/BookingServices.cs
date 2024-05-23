using MealBookingAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MealBookingAPI.Application.Services.IServices;
using MealBookingAPI.Data.Models;
using MealBookingAPI.Application.DTOs;

namespace MealBookingAPI.Application.Services
{
    public class BookingServices: IBookingServices
    {
        private readonly AppDbContext _dbContext;

        public BookingServices(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<List<DateTime>> GetDates(int user_id)
        {
            return await _dbContext.Booking
                .Where(b => b.User_Id == user_id)
                .Select(b => b.Booking_For_Date_Time)
                .ToListAsync();
        }
    }
}
