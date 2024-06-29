using System.Linq.Expressions;
using Core.Entities;
using Core.Helpers;

namespace Core.Interfaces;

public interface IBaseService<T> where T : BaseEntity
{

    Task<IResultList<TDto>> GetListAsync<TDto>(ISpecification<T,TDto> specification);

    Task<TDto> GetByIdAsync<TDto>(int id, ISpecification<T,TDto> specification);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
}