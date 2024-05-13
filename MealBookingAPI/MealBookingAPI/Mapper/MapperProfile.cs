using AutoMapper;
using MealBookingAPI.Application.DTOs;
using MealBookingAPI.Data.Models;

namespace MealBookingAPI.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<BookingRequestDTO, Booking>().ReverseMap();
            CreateMap<Booking,BookingRequestDTO>().ReverseMap();
        }
    }
}
