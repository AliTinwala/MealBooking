using MEAL_2024_API.Context;
using MEAL_2024_API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace MEAL_2024_API.Repositories
{
    public class Repository<TEntity>: IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

   
    }
}
