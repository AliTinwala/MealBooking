using MealBookingAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace MealBookingAPI.Application.Services.IServices
{
    public interface IBookingServices
    {
        Task<List<DateTime>> GetDates(int user_id);
    }
}
