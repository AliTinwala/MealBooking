namespace MealBookingAPI.Application.Services.IServices
{
    public interface INotificationServices
    {
        Task<IEnumerable<string>> GetNoticationsForUser(Guid user_id);
        Task<int> GetUnreadNotificationsForUser(Guid user_id);
        Task<int> SetReadNotificationForUser(Guid notification_id);
    }
}
