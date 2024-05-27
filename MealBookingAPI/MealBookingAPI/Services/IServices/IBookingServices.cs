using MealBookingAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace MealBookingAPI.Application.Services.IServices
{
    public interface IBookingServices
    {
        Task<IEnumerable<DateTime>> GetBookingForDates(Guid user_id);
    }
}
