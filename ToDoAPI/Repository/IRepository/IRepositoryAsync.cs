using System.Linq.Expressions;

namespace ToDoAPI.Repository.IRepository
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<T> GetAsync(int id);

        Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
            );

        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null);

        Task AddAsync(T entity);
        Task<T> AddAndGetAsync(T entity);
        Task RemoveAsync(int id);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entity); 
        Task CreateAsync(T entity);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null);
    }
}
