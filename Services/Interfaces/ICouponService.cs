using MEAL_2024_API.Models;

namespace MEAL_2024_API.Services.Interfaces
{
    public interface ICouponService
    {
        Task<CouponModel> GenerateCoupon(Guid bookingId);
        Task<CouponModel> GetCouponByBookingId(Guid bookingId);
        Task<bool> RedeemCoupon(Guid couponId);

        Task DeleteCouponByBookingId(Guid bookingId);
    }
}
