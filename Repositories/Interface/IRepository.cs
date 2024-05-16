namespace MEAL_2024_API.Repositories.Interface
{
    public interface IRepository<TEntity>
    {
        public Task<IEnumerable<TEntity>> GetAll();
    }
}
