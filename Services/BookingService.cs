using MEAL_2024_API.DTO;
using MEAL_2024_API.Models;
using MEAL_2024_API.Repositories.Interface;
using MEAL_2024_API.Services.Interfaces;

namespace MEAL_2024_API.Services
{
    public class BookingService:IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICouponService _couponService;
        public BookingService(IBookingRepository bookingRepository, 
            ICouponService couponService)
        {
            _bookingRepository = bookingRepository;
            _couponService = couponService;
        }

        public async Task<IEnumerable<BookingDTO>> GetBookingsByUserIdAsync(Guid userId)
        {
            var bookings = await _bookingRepository.GetBookingsByUserIdAsync(userId);
            return bookings.Select(b => new BookingDTO
            {
                BookingId = b.BookingId,
                UserId = b.UserId,
                MealType = b.MealType,
                BookingDate = b.BookingDate,
                CreatedDate = b.CreatedDate,
                IsCancelled = b.IsCancelled
            });
        }

        public async Task<IEnumerable<BookingDTO>> GetBookingsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var bookings = await _bookingRepository.GetBookingsByDateRangeAsync(startDate, endDate);
            return bookings.Select(b => new BookingDTO
            {
                BookingId = b.BookingId,
                UserId = b.UserId,
                MealType = b.MealType,
                BookingDate = b.BookingDate,
                CreatedDate = b.CreatedDate,
                IsCancelled = b.IsCancelled
            });
        }

        public async Task CreateBookingAsync(BookingCreateDTO bookingCreateDTO)
        {
            var today = DateTime.UtcNow.Date;
            var bookingDateTimeLimit = today.AddDays(1).AddHours(22); // 10 PM of the previous day

            if (bookingCreateDTO.StartDate.Date < bookingDateTimeLimit.Date)
            {
                throw new InvalidOperationException("Bookings can only be made before 10 PM of the previous day.");
            }

            if ((bookingCreateDTO.EndDate.Date - bookingCreateDTO.StartDate.Date).TotalDays > 30)
            {
                throw new InvalidOperationException("The date range for bookings cannot exceed 30 days.");
            }

            var dates = Enumerable.Range(0, 1 + bookingCreateDTO.EndDate.Date.Subtract(bookingCreateDTO.StartDate.Date).Days)
                .Select(offset => bookingCreateDTO.StartDate.Date.AddDays(offset))
                .ToArray();

            foreach (var date in dates)
            {
                // Skip Saturdays and Sundays
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }

                //var existingBooking = await _bookingRepository.GetBookingsByUserIdAsync(bookingCreateDTO.UserId, date);
                // Check for existing bookings for the same date and meal type
                var existingBooking = await _bookingRepository.GetBookingsByUserIdAndMealAsync(bookingCreateDTO.UserId, date, bookingCreateDTO.MealType);

                if (existingBooking != null)
                {
                    throw new InvalidOperationException($"User {bookingCreateDTO.UserId} already has a booking on {date.ToShortDateString()}.");
                }

                var booking = new BookingModel
                {
                    BookingId = Guid.NewGuid(),
                    UserId = bookingCreateDTO.UserId,
                    MealType = bookingCreateDTO.MealType,
                    BookingDate = date,
                    CreatedDate = DateTime.UtcNow,
                    IsCancelled = false
                };
                await _bookingRepository.Add(booking);
                await _couponService.GenerateCoupon(booking.BookingId);
            }
            await _bookingRepository.SaveChanges();
        }

        public async Task UpdateBookingAsync(Guid bookingId, BookingDTO bookingDTO)
        {
            var booking = new BookingModel
            {
                BookingId = bookingDTO.BookingId,
                UserId = bookingDTO.UserId,
                MealType = bookingDTO.MealType,
                BookingDate = bookingDTO.BookingDate,
                CreatedDate = bookingDTO.CreatedDate,
                IsCancelled = bookingDTO.IsCancelled
            };
            await _bookingRepository.Update(booking);
        }

        public async Task DeleteBookingAsync(Guid userId, DateTime bookingDate, string mealType)
        {
            // Fetch the booking based on userId, bookingDate, and mealType
            var booking = await _bookingRepository.GetBookingsByUserIdAndMealAsync(userId, bookingDate, mealType);
            if (booking != null)
            {
                // Check if the cancellation is before 10 PM of the previous day
                var bookingDateTimeLimit = booking.BookingDate.AddDays(-1).AddHours(22); // 10 PM of the previous day
                if (DateTime.UtcNow > bookingDateTimeLimit)
                {
                    throw new InvalidOperationException("Bookings can only be cancelled before 10 PM of the previous day.");
                }

                booking.IsCancelled = true;
                await _bookingRepository.SaveChanges();

                // Delete the associated coupon
                await _couponService.DeleteCouponByBookingId(booking.BookingId);
            }
            else
            {
                throw new InvalidOperationException($"No booking found for {mealType} on {bookingDate.ToShortDateString()} for user {userId}.");
            }
        }

        //public async Task DeleteBookingAsync(Guid bookingId, string mealType)
        //{
        //    var booking = await _bookingRepository.GetById(bookingId);
        //    if (booking != null)
        //    {
        //        // Check if the cancellation is before 10 PM of the previous day
        //        var bookingDateTimeLimit = booking.BookingDate.AddDays(-1).AddHours(22); // 10 PM of the previous day
        //        if (DateTime.UtcNow > bookingDateTimeLimit)
        //        {
        //            throw new InvalidOperationException("Bookings can only be cancelled before 10 PM of the previous day.");
        //        }

        //        booking.IsCancelled = true;
        //        await _bookingRepository.SaveChanges();

        //        // Delete the associated coupon
        //        await _couponService.DeleteCouponByBookingId(bookingId);
        //    }
        //}
    }
}
