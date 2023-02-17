using System.Linq.Expressions;

namespace UserCrud.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
        
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync(int skip, int limit, IEnumerable<Expression<Func<T, bool>>> predicates = null);
    }
}
