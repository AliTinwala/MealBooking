using AutoMapper;
using MealBookingAPI.Application.DTOs;
using MealBookingAPI.Data.Models;
using MealBookingAPI.Data.Repository;
using MealBookingAPI.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace MealBookingAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : Controller
    {

        private readonly IRepository<Notification> _repository;
        private readonly IMapper _mapper;

        public NotificationController(IRepository<Notification> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("{user_id}")]
        public async Task<IActionResult> CreateNotification([FromBody]NotificationResponseDTO request, [FromRoute]Guid user_id)
        {
            if (ModelState.IsValid)
            {
                var notification = _mapper.Map<NotificationResponseDTO, Notification>(request);
                notification.UserId = user_id;
                var inserted = await _repository.InsertAsync(notification);
                if (inserted <= 0)
                {
                    return BadRequest(notification);
                }
                else 
                {
                    return Ok(notification);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{user_id}")]
        public async Task<IActionResult> GetNotificationsByUserId(Guid user_id)
        {
            var notificationList = await _repository.GetNotificationsByUserId(user_id);
            var notificationDto = _mapper.Map<List<Notification>, List<NotificationRequestDTO>>(notificationList.ToList());
            if (notificationDto != null)
            {
                return Ok(notificationDto);
            }
            else
            {
                return NotFound(notificationDto);
            }
        }

        [HttpPut("{notification_id}")]
        public async Task<IActionResult> SetNotificationAsRead([FromRoute] Guid notification_id)
        {
            var updated = await _repository.SetReadNotificationForUser(notification_id);
            var notificationDto = _mapper.Map<Notification, NotificationResponseDTO>(updated);
            if (notificationDto != null)
            {
                return Ok(notificationDto);
            }
            else
            {
                return BadRequest(notificationDto);
            }
    }
    }
}
