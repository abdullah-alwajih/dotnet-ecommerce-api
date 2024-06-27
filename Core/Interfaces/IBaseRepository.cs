using System.Linq.Expressions;
using Core.Helpers;

namespace Core.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<IResultList<TResult>> GetListAsync<TResult>(
        Expression<Func<TEntity, bool>>? predicate = null,
        Expression<Func<TEntity, TResult>>? select = null,
        PaginationQueryParameters? paginationQueryParameters = null

    );

    Task<TResult> GetByIdAsync<TResult>(int id, Expression<Func<TEntity, TResult>> selector);

    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
}