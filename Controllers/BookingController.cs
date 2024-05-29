using MEAL_2024_API.DTO;
using MEAL_2024_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MEAL_2024_API.Controllers
{
  
        [ApiController]
        [Route("api/[controller]")]
        public class BookingController : ControllerBase
        {
            private readonly IBookingService _bookingService;

            public BookingController(IBookingService bookingService)
            {
                _bookingService = bookingService;
            }

            [HttpGet("user/{userId}")]
            public async Task<ActionResult<IEnumerable<BookingDTO>>> GetBookingsByUserId(Guid userId)
            {
                var bookings = await _bookingService.GetBookingsByUserIdAsync(userId);
                return Ok(bookings);
            }

            [HttpGet("date-range")]
            public async Task<ActionResult<IEnumerable<BookingDTO>>> GetBookingsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
            {
                var bookings = await _bookingService.GetBookingsByDateRangeAsync(startDate, endDate);
                return Ok(bookings);
            }

        [HttpPost("quick-booking")]
        public async Task<ActionResult> QuickBooking([FromBody] QuickBookingDTO quickBookingDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _bookingService.QuickBookingAsync(quickBookingDTO);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
            public async Task<ActionResult> CreateBooking([FromBody] BookingCreateDTO bookingCreateDTO)
            {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _bookingService.CreateBookingAsync(bookingCreateDTO);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            //    await _bookingService.CreateBookingAsync(bookingCreateDTO);
            //    return Ok();
        }

            [HttpPut("{bookingId}")]
            public async Task<ActionResult> UpdateBooking(Guid bookingId, [FromBody] BookingDTO bookingDTO)
            {
                await _bookingService.UpdateBookingAsync(bookingId, bookingDTO);
                return Ok();
            }

            [HttpDelete]
            public async Task<ActionResult> DeleteBooking([FromQuery] Guid userId, [FromQuery] DateTime bookingDate, [FromQuery] string mealType)
            {
            try
            {
                await _bookingService.DeleteBookingAsync(userId, bookingDate, mealType);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while cancelling the booking.", detail = ex.Message });
            }
        }
        }
    }

