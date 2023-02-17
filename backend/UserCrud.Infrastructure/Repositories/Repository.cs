namespace UserCrud.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using System.Linq.Expressions;
    using UserCrud.Domain.Entities;
    using UserCrud.Domain.Repositories;
    using UserCrud.Infrastructure.Helpers;

    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> _entities;

        public Repository(DbSet<T> entity)
        {
            _entities = entity;
        }
        public async Task AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() => { _entities.Update(entity); });
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => { _entities.Remove(entity); });
        }

        public async Task<IEnumerable<T>> GetAllAsync(int skip, int limit, IEnumerable<Expression<Func<T, bool>>> predicates = null)
        {
            return await _entities.Filter(predicates).Skip(skip).Take(limit).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
