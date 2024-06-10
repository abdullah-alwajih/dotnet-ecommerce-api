using System.Linq.Expressions;
using Core.Helpers;

namespace Core.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<PaginatedList<TResult>> GetListAsync<TResult>(
        Expression<Func<TEntity, bool>>? predicate = null,
        Expression<Func<TEntity, TResult>>? select = null,
        int pageNumber = 1,
        int pageSize = 10);

    Task<TResult> GetByIdAsync<TResult>(int id, Expression<Func<TEntity, TResult>> selector);

    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
}