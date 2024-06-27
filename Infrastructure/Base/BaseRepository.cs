using System.Linq.Expressions;
using Core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Core.Interfaces;

public class BaseRepository<TEntity>(DbContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DbContext Context = context;
    protected readonly DbSet<TEntity> DbSet = context.Set<TEntity>();

    public virtual async Task<IResultList<TResult>> GetListAsync<TResult>(
        Expression<Func<TEntity, bool>>? predicate = null,
        Expression<Func<TEntity, TResult>>? select = null,
        PaginationQueryParameters? paginationQueryParameters = null
    )
    {
        var baseQuery = DbSet.AsNoTracking();

        // Apply the predicate if it is provided
        if (predicate != null) baseQuery = baseQuery.Where(predicate);

        // Apply the projection if provided, else project to the same type
        var finalQuery = select != null ? baseQuery.Select(select) : baseQuery.Cast<TResult>();


        // Compute total count of items matching the predicate (before pagination)

        // Apply pagination
        if (paginationQueryParameters != null)
        {
            var count = await finalQuery.CountAsync();
            var pageNumber = paginationQueryParameters.PageNumber;
            var pageSize = paginationQueryParameters.PageSize; // Default to a large number if pageSize is not provided

            finalQuery = finalQuery.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            var items = await finalQuery.ToListAsync();

            // Return the paginated list
            return new PaginatedList<TResult>(items, count, pageNumber, pageSize);
        }
        else
        {
            var items = await finalQuery.ToListAsync();
            return new ResultList<TResult>(items);
        }
    }

    public virtual async Task<TResult> GetByIdAsync<TResult>(int id, Expression<Func<TEntity, TResult>> selector)
    {
        var query = DbSet.AsNoTracking();

        var result = await query.Where(entity => EF.Property<int>(entity, "Id") == id)
            .Select(selector)
            .FirstOrDefaultAsync();

        return result ?? throw new KeyNotFoundException($"Entity with ID {id} not found.");
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task RemoveAsync(TEntity entity)
    {
        DbSet.Remove(entity);
        await Context.SaveChangesAsync();
    }

}