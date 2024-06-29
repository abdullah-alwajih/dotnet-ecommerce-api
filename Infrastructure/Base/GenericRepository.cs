using System.Linq.Expressions;
using AutoMapper;
using Core.Entities;
using Core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Core.Interfaces;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    protected GenericRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public virtual async Task<IResultList<TDto>> GetListAsync<TDto>(ISpecification<TEntity, TDto> specification)
    {
        var baseQuery = _dbSet.AsNoTracking();

        // Apply the predicate if it is provided
        if (specification.Criteria != null) baseQuery = baseQuery.Where(specification.Criteria);


        // Apply the projection if provided, else project to the same type
        var finalQuery = specification.Select != null ? baseQuery.Select(specification.Select) : baseQuery.Cast<TDto>();



        // Apply pagination
        if (specification.Pagination != null)
        {
            var count = await finalQuery.CountAsync();
            var pageNumber = specification.Pagination.PageNumber;
            var pageSize = specification.Pagination.PageSize; // Default to a large number if pageSize is not provided

            finalQuery = finalQuery.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            var items = await finalQuery.ToListAsync();

            // Return the paginated list
            return new PaginatedList<TDto>(items, count, pageNumber, pageSize);
        }
        else
        {
            var items = await finalQuery.ToListAsync();
            return new ResultList<TDto>(items);
        }
    }


    public virtual async Task<TDto> GetByIdAsync<TDto>(int id, ISpecification<TEntity, TDto> specification)
    {
        var query = _dbSet.AsNoTracking();

        // If selector is null, use an expression that selects the entity itself
        specification.Select ??= query => (TDto)(object)query;

        var result = await query
            .Where(entity => EF.Property<int>(entity, "Id") == id)
            .Select(specification.Select)
            .FirstOrDefaultAsync();

        return result ?? throw new KeyNotFoundException($"{typeof(TEntity).Name} with ID {id} not found.");

    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task RemoveAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
    
    private IQueryable<TResult> DefaultProjection<TResult>(IQueryable<TEntity> query)
    {
        // Check if TResult is the same as TEntity, allowing direct casting
        if (typeof(TResult) == typeof(TEntity))
            return (IQueryable<TResult>)query;
        throw new InvalidOperationException(
            "A select expression must be provided when TResult is not of type TEntity.");
    }
    
}