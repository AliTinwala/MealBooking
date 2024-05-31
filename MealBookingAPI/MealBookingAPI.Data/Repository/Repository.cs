using AutoMapper;
using MealBookingAPI.Data.Models;
using MealBookingAPI.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MealBookingAPI.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _dbContext;
        public Repository(AppDbContext context, IMapper mapper)
        {
            _dbContext = context;
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }


        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<int> UpdateAsync(Guid id, TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<DateTime>> GetBookingForDates(Guid user_id)
        {
            return await _dbContext.Booking
                .Where(b => b.UserId == user_id)
                .Select(b => b.BookingForDate)
                .ToListAsync();
        }

        public async Task<int> GetCountOfUnreadNotificationOfUser(Guid user_id)
        {
            return await _dbContext.Notification
                .Where(n => n.UserId == user_id)
                .Where(n => n.isRead == false)
                .Select(n => n.NotificationId)
                .CountAsync();
        }

        public async Task<IEnumerable<string>> GetNotificationsForUser(Guid user_id)
        {
            return await _dbContext.Notification
                .Where(b => b.UserId == user_id)
                .Select(b => b.Message)
                .ToListAsync();
        }

        public async Task<int> SetReadNotificationForUser(Guid notification_id)
        {
            var notification =  await _dbContext.Notification
                .FirstOrDefaultAsync(n => n.NotificationId == notification_id);
            
            if(notification != null)
            {
                notification.isRead = true;

                return await _dbContext.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }
    }
}
