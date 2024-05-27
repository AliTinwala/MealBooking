using MealBookingAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MealBookingAPI.Application.Services.IServices;
using MealBookingAPI.Data.Models;
using MealBookingAPI.Application.DTOs;
using MealBookingAPI.Data.Repository.IRepository;
using AutoMapper;

namespace MealBookingAPI.Application.Services
{
    public class BookingServices: IBookingServices
    {
        private readonly IRepository<Booking> _repository;

        public BookingServices(IRepository<Booking> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DateTime>> GetBookingForDates(Guid user_id)
        {
            var dates = await _repository.GetBookingForDates(user_id);
            return dates;
        }
    }
}
