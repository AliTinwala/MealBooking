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
            CreateMap<Task<Booking>, Booking>().ReverseMap();
            CreateMap<UserRequestDTO, User>().ReverseMap();
            CreateMap<NotificationRequestDTO, Notification>().ReverseMap();
            CreateMap<NotificationResponseDTO, Notification>().ReverseMap();
        }
    }
}
