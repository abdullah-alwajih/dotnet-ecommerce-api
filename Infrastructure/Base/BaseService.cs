using System.Linq.Expressions;
using Core.Entities;

namespace Core.Interfaces;

public class BaseService<T, TDto>(IGenericRepository<T, TDto> repository) : IBaseService<T, TDto>
    where T : BaseEntity
{

    public virtual async Task<IResultList<TDto>> GetListAsync(ISpecification<T> specification)
    {
        return await repository.GetListAsync(specification);
    }
    

    public virtual async Task<TDto> GetByIdAsync(int id)
    {
        return await repository.GetByIdAsync(id);
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