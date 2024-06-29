using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<TEntity>
{
    Expression<Func<TEntity, bool>>? Criteria { get; }
  

    
    Pagination? Pagination { get; }

}
