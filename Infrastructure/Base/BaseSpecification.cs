using System.Linq.Expressions;

namespace Core.Interfaces;

public class BaseSpecification<TEntity, TDto>(
    Expression<Func<TEntity, bool>>?  criteria = null,
    Expression<Func<TEntity, object>>? includes = null,
    Expression<Func<TEntity, TDto>>? select = null,
    Pagination? pagination= null
) : ISpecification<TEntity, TDto>
{
    public Expression<Func<TEntity, bool>>? Criteria { get; } = criteria;
    public Expression<Func<TEntity, object>>? Includes { get; } = includes;
    public Expression<Func<TEntity, TDto>>? Select { get; set; } = select;
    public Pagination? Pagination { get; } = pagination;

    // protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
    // {
    //     Includes.Add(includeExpression);
    // }
}