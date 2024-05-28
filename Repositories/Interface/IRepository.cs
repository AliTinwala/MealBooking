namespace MEAL_2024_API.Repositories.Interface
{
    public interface IRepository<TEntity>
    {
        public Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(Guid id);
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(Guid id);
        Task SaveChanges();

    }
}
