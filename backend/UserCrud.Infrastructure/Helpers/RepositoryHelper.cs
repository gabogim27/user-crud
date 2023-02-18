namespace UserCrud.Infrastructure.Helpers
{
    using Microsoft.EntityFrameworkCore;
    using System.Linq.Expressions;

    public static class RepositoryHelper
    {
        public static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> source, IEnumerable<Expression<Func<TEntity, bool>>> predicates) where TEntity : class
        {
            if (predicates != null && predicates.Any())
            {
                foreach (var predicate in predicates)
                {
                    source = source.Where(predicate);
                }
            }

            return source;
        }
    }
}
