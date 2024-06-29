using System.Linq.Expressions;
using Core.Entities;

namespace Core.Interfaces;

public class BaseService<T>(IGenericRepository<T> repository) : IBaseService<T>
    where T : BaseEntity
{

    public virtual async Task<IResultList<TDto>> GetListAsync<TDto>(ISpecification<T, TDto> specification)
    {
        return await repository.GetListAsync(specification);
    }
    

    public virtual async Task<TDto> GetByIdAsync<TDto>(int id, ISpecification<T, TDto> specification)
    {
        return await repository.GetByIdAsync<TDto>(id, specification);
    }

    public virtual async Task AddAsync(T entity)
    {
        await repository.AddAsync(entity);
    }

    public virtual async Task UpdateAsync(T entity)
    {
        await repository.UpdateAsync(entity);
    }

    public virtual async Task RemoveAsync(T entity)
    {
        await repository.RemoveAsync(entity);
    }
}