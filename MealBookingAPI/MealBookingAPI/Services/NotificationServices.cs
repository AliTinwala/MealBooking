using MealBookingAPI.Application.Services.IServices;
using MealBookingAPI.Data.Models;
using MealBookingAPI.Data.Repository.IRepository;

namespace MealBookingAPI.Application.Services
{
    public class NotificationServices : INotificationServices
    {
        private readonly IRepository<Notification> _repository;

        public NotificationServices(IRepository<Notification> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<string>> GetNoticationsForUser(Guid user_id)
        {
            var messages = await _repository.GetNotificationsForUser(user_id);
            return messages;
        }

        public async Task<int> GetUnreadNotificationsForUser(Guid user_id)
        {
            var unread = await _repository.GetCountOfUnreadNotificationOfUser(user_id);
            return unread;
        }

        public async Task<int> SetReadNotificationForUser(Guid notification_id)
        {
            var read = await _repository.SetReadNotificationForUser(notification_id);
            return read;
        }
    }
}
