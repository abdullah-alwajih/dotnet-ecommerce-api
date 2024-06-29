using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Entities;
using Core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Core.Interfaces;

public class GenericRepository<TEntity, TDto> : IGenericRepository<TEntity, TDto> where TEntity : BaseEntity
{
    private readonly DbContext _context;
    private readonly IMapper _mapper;
    private readonly DbSet<TEntity> _dbSet;

    protected GenericRepository(DbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _dbSet = _context.Set<TEntity>();
    }

    public virtual async Task<IResultList<TDto>> GetListAsync(ISpecification<TEntity> specification)
    {
        var baseQuery = _dbSet.AsNoTracking();

        // Apply the predicate if it is provided
        if (specification.Criteria != null) baseQuery = baseQuery.Where(specification.Criteria);


        // Apply the projection if provided, else project to the same type
        var finalQuery = baseQuery.ProjectTo<TDto>(_mapper.ConfigurationProvider);



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


    public virtual async Task<TDto> GetByIdAsync(int id)
    {
        var result = await _dbSet.AsNoTracking()
            .Where(entity => EF.Property<int>(entity, "Id") == id)
            .ProjectTo<TDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        
        return result ??
               throw new KeyNotFoundException($"{typeof(TEntity).Name} with ID {id} not found.");
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
}