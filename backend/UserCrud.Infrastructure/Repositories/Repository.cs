namespace UserCrud.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using System.Linq.Expressions;
    using UserCrud.Domain.Entities;
    using UserCrud.Domain.Repositories;
    using UserCrud.Infrastructure.Helpers;

    public class Repository<TEntity, TContext> : IRepository<TEntity> 
        where TEntity : BaseEntity
        where TContext : DbContext
    {
        private readonly TContext _context;

        public Repository(TContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => { _context.Set<TEntity>().Attach(entity); });
            await Task.Run(() => { _context.Entry(entity).State = EntityState.Modified; });
            await _context.SaveChangesAsync();
            //_context.Entry<TEntity>(entity).Reload();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => { _context.Set<TEntity>().Remove(entity); });
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(int skip, int limit, IEnumerable<Expression<Func<TEntity, bool>>> predicates = null)
        {
            return await _context.Set<TEntity>().Filter(predicates).Skip(skip).Take(limit).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AnyAsync(predicate);
        }
    }
}
