using AutoMapper;
using MealBookingAPI.Application.DTOs;
using MealBookingAPI.Application.Services.IServices;
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
        private readonly INotificationServices _notificationServices;

        public NotificationController(IRepository<Notification> repository, IMapper mapper, INotificationServices notificationServices)
        {
            _repository = repository;
            _mapper = mapper;
            _notificationServices = notificationServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification(NotificationRequestDTO request, Guid user_id)
        {
            if (ModelState.IsValid)
            {
                var notification = _mapper.Map<NotificationRequestDTO, Notification>(request);
                notification.UserId = user_id;
                var inserted = await _repository.InsertAsync(notification);
                if (inserted <= 0)
                {
                    return BadRequest("Notification not added");
                }
                else
                {
                    return Ok("Notification added");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{user_id}")]
        public async Task<IActionResult> GetUnreadNotificationsByUserId(Guid user_id)
        {
            var unread = await _notificationServices.GetUnreadNotificationsForUser(user_id);
            return Ok(unread);
        }

        [HttpGet]
        public async Task<IActionResult> GetNotificationByUserId(Guid user_id)
        {
            var notifications = await _notificationServices.GetNoticationsForUser(user_id);
            if (notifications.Count() > 0)
            {
                return Ok(notifications);
            }
            else
            {
                return NotFound("Notifications not found");
            }
        }

        [HttpPut("{notification_id}")]
        public async Task<IActionResult> SetNotificationAsRead([FromRoute] Guid notification_id)
        {
            var updated = await _notificationServices.SetReadNotificationForUser(notification_id);

            if (updated > 0)
            {
                return Ok("Notification updated");
            }
            else
            {
                return BadRequest("Notification not updated");
            }
    }
    }
}
