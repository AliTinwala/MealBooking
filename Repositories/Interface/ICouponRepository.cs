using MEAL_2024_API.Models;

namespace MEAL_2024_API.Repositories.Interface
{
    public interface ICouponRepository:IRepository<CouponModel>
    {
        Task<CouponModel> GetByBookingIdAsync(Guid bookingId);
        Task<bool> CouponExistsAsync(Guid bookingId);
    }
}
