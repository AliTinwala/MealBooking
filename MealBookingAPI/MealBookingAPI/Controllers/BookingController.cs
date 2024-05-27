using AutoMapper;
using MealBookingAPI.Application.DTOs;
using MealBookingAPI.Application.Services.IServices;
using MealBookingAPI.Data.Models;
using MealBookingAPI.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MealBookingAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly IRepository<Booking> _repository;
        private readonly IMapper _mapper;
        private readonly IBookingServices _bookingServices;

        public BookingController(IRepository<Booking> repository, IMapper mapper, IBookingServices bookingServices)
        {
            _repository = repository;
            _mapper = mapper;
            _bookingServices = bookingServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTEntity(BookingRequestDTO request,Guid user_id)
        {
            if (ModelState.IsValid)
            {
                var booking = _mapper.Map<BookingRequestDTO, Booking>(request);
                booking.UserId = user_id;
                var inserted = await _repository.InsertAsync(booking);
                if (inserted <= 0)
                {
                    return BadRequest("Booking not added");
                }
                else
                {
                    return Ok("Booking added");
                }
            }
            else
            {
                return BadRequest("Booking not added");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            var bookingList = await _repository.GetAll();
            var bookingDto = _mapper.Map<List<Booking>, List<BookingRequestDTO>>(bookingList.ToList());
            if (bookingDto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(bookingDto);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveBooking(Guid id)
        { 
            var booking = await _repository.GetByIdAsync(id);
            var deleted = await _repository.DeleteAsync(booking);
            if (deleted > 0)
            {
                return Ok("Deleted Successfully");
            }
            else
            {
                return BadRequest("Deletion unsuccessful");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking([FromRoute] Guid id, [FromBody] BookingRequestDTO bookingDto)
        {
            var existingBooking = await _repository.GetByIdAsync(id);

            if (existingBooking == null)
            {
                throw new InvalidOperationException("Booking not found");
            }

            // Update the existingProduct entity with data from productDto
            _mapper.Map(bookingDto, existingBooking);

            var updated = await _repository.UpdateAsync(id, existingBooking);

            if (updated > 0)
            {
                return Ok("Booking updated");
            }
            else
            {
                return BadRequest("Booking not updated");
            }
        }

        [HttpGet("{user_id}")]
        public async Task<IActionResult> GetBookingDates(Guid user_id)
        {
            var dates = await _bookingServices.GetBookingForDates(user_id);
            if (dates.Count() > 0)
            {
                return Ok(dates);
            }
            else
            {
                return NotFound("Dates not found");
            }

        }
    }
}
