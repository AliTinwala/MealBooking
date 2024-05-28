using MEAL_2024_API.Context;
using MEAL_2024_API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace MEAL_2024_API.Repositories
{
    public class Repository<TEntity>: IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        protected DbSet<TEntity> DbSet => _dbSet;
        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id); ;
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            _dbSet.Update(entity);
            await SaveChanges();
        }
    }
}
