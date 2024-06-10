using System.Linq.Expressions;
using Core.Helpers;

namespace Core.Interfaces;

public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
{
    protected readonly IBaseRepository<TEntity> Repository;

    public BaseService(IBaseRepository<TEntity> repository)
    {
        Repository = repository;
    }

    public virtual async Task<PaginatedList<TResult>> GetListAsync<TResult>(
        Expression<Func<TEntity, bool>>? predicate = null,
        Expression<Func<TEntity, TResult>>? select = null,
        int pageNumber = 1,
        int pageSize = 10)
    {
        return await Repository.GetListAsync(predicate, select, pageNumber, pageSize);
    }

    public virtual async Task<TResult> GetByIdAsync<TResult>(int id, Expression<Func<TEntity, TResult>> selector)
    {
        return await Repository.GetByIdAsync(id, selector);
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await Repository.AddAsync(entity);
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        await Repository.UpdateAsync(entity);
    }

    public virtual async Task RemoveAsync(TEntity entity)
    {
        await Repository.RemoveAsync(entity);
    }
}