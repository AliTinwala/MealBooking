using MealBookingAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MealBookingAPI.Data.Repository.IRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<int> InsertAsync(TEntity entity);
        Task<int> UpdateAsync(Guid id, TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<DateTime>> GetBookingForDates(Guid user_id);
        Task<IEnumerable<Notification>> GetNotificationsByUserId(Guid user_id);
        Task<Notification> SetReadNotificationForUser(Guid notification_id);
    }
}
