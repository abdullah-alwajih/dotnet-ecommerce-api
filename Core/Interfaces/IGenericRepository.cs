using System.Linq.Expressions;
using Core.Entities;
using Core.Helpers;

namespace Core.Interfaces;

public interface IGenericRepository<T, TDto> where T : BaseEntity
{
    Task<IResultList<TDto>> GetListAsync(ISpecification<T> specification);

    Task<TDto> GetByIdAsync(int id);

    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
}