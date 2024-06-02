using MEAL_2024_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MEAL_2024_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController :ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpPost("{bookingId}")]
        public async Task<IActionResult> GenerateCoupon(Guid bookingId)
        {
            try
            {
                var coupon = await _couponService.GenerateCoupon(bookingId);
                return Ok(coupon);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetCouponByBookingId(Guid bookingId)
        {
            var coupon = await _couponService.GetCouponByBookingId(bookingId);
            if (coupon == null)
                return NotFound();

            return Ok(coupon);
        }

        [HttpPut("redeem/{couponId}")]
        public async Task<IActionResult> RedeemCoupon(Guid couponId)
        {
            var success = await _couponService.RedeemCoupon(couponId);
            return Ok(success);
        }
    }
}
