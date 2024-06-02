using MEAL_2024_API.Helpers;
using MEAL_2024_API.Models;
using MEAL_2024_API.Repositories.Interface;
using MEAL_2024_API.Services.Interfaces;

namespace MEAL_2024_API.Services
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository _couponRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IQrCodeHelper _qrCodeHelper;

        public CouponService(ICouponRepository couponRepository, IQrCodeHelper qrCodeHelper,
            IBookingRepository bookingRepository)
        {
            _couponRepository = couponRepository;
            _qrCodeHelper = qrCodeHelper;
            _bookingRepository = bookingRepository;
        }

        public async Task DeleteCouponByBookingId(Guid bookingId)
        {
            var coupon = await _couponRepository.GetByBookingIdAsync(bookingId);
            if (coupon != null)
            {
                coupon.IsDeleted = true;
                await _couponRepository.SaveChanges();
            }
        }

        public async Task<CouponModel> GenerateCoupon(Guid bookingId)
        {
            if (await _couponRepository.CouponExistsAsync(bookingId))
                throw new Exception("Coupon already exists for this booking.");

            var booking = await _bookingRepository.GetById(bookingId);
            if (booking == null)
                throw new Exception("Booking not found.");

            var qrCodeBase64 = _qrCodeHelper.GenerateQrCode(bookingId.ToString());

            var coupon = new CouponModel
            {
                CouponId = Guid.NewGuid(),
                BookingId = bookingId,
                QrCodeBase64 = qrCodeBase64,
                CreatedDate = DateTime.UtcNow,
                IsRedeemed = false,
                IsDeleted = false
            };

            await _couponRepository.Add(coupon);

            return coupon;
        }

        public async Task<CouponModel> GetCouponByBookingId(Guid bookingId)
        {
            return await _couponRepository.GetByBookingIdAsync(bookingId);
        }

        public async Task<Object> RedeemCoupon(Guid couponId)
        {
            var coupon = await _couponRepository.GetById(couponId);
            if (coupon == null || coupon.IsRedeemed)
                return (new
                {
                    Success=false,
                    Message = "Coupon already redeemed"
                });

            coupon.IsRedeemed = true;
            await _couponRepository.Update(coupon);
            return (new
            {
                Success = true,
                Message = "Coupon Redeemed Successfully!"
            });
        }
    }
}
