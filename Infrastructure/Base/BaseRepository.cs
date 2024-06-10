using System.Linq.Expressions;
using Core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Core.Interfaces;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public BaseRepository(DbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public virtual async Task<PaginatedList<TResult>> GetListAsync<TResult>(
        Expression<Func<TEntity, bool>>? predicate = null,
        Expression<Func<TEntity, TResult>>? select = null,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var query = DbSet.AsNoTracking();

        // Apply the predicate if it is provided
        if (predicate != null) query = query.Where(predicate);

        // Determine the query projection
        var selectedQuery = select != null
            ? query.Select(select)
            : DefaultProjection<TResult>(query);

        // Compute total count of items matching the predicate (before pagination)
        var count = await selectedQuery.CountAsync();

        // Apply pagination
        var items = await selectedQuery.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        // Return the paginated list
        return new PaginatedList<TResult>(items, count, pageNumber, pageSize);
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

    private IQueryable<TResult> DefaultProjection<TResult>(IQueryable<TEntity> query)
    {
        // Check if TResult is the same as TEntity, allowing direct casting
        if (typeof(TResult) == typeof(TEntity))
            return (IQueryable<TResult>)query;
        throw new InvalidOperationException(
            "A select expression must be provided when TResult is not of type TEntity.");
    }
}