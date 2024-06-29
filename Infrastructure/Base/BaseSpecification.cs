using System.Linq.Expressions;

namespace Core.Interfaces;

public class BaseSpecification<TEntity>(
    Expression<Func<TEntity, bool>>?  criteria = null,
   
    Pagination? pagination= null
) : ISpecification<TEntity>
{
    public Expression<Func<TEntity, bool>>? Criteria { get; } = criteria;

    public Pagination? Pagination { get; } = pagination;

    // protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
    // {
    //     Includes.Add(includeExpression);
    // }
}