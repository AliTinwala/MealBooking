using Microsoft.AspNetCore.Mvc;

namespace MealBookingAPI.Application.Services.IServices
{
    public interface IBookingServices
    {
        Task<ActionResult<bool>> BookingExists(DateTime date);
    }
}
