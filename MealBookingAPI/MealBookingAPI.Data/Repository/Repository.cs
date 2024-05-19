using AutoMapper;
using MealBookingAPI.Data.Models;
using MealBookingAPI.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MealBookingAPI.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;
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


        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<int> UpdateAsync(int id, TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }
    }
}
