using MealBookingAPI.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MealBookingAPI.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;
        public Repository(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            var deleted = await _dbContext.SaveChangesAsync();
            if (deleted >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            return entity;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            var update = _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
            if (entity.Equals(update))
            {
                return true;
            }
            else 
            { 
                return false; 
            }
        }
    }
}
