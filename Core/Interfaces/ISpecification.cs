using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<TEntity, TDto>
{
    Expression<Func<TEntity, bool>>? Criteria { get; }
  
    Expression<Func<TEntity, object>>? Includes { get; }
    
    Expression<Func<TEntity, TDto>>? Select { get; set; }
    
    Pagination? Pagination { get; }

}
