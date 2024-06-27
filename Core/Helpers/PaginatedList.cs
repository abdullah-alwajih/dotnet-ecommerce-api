using Core.Interfaces;

namespace Core.Helpers;

public class PaginatedList<T>(IEnumerable<T> items, int count, int pageNumber, int pageSize) : IResultList<T>
{
    public IEnumerable<T> Items { get; set; } = items;

    public PaginationMetadata Metadata { get; set; } = new()
    {
        TotalCount = count,
        PageSize = pageSize,
        CurrentPage = pageNumber,
        TotalPages = (int)Math.Ceiling(count / (double)pageSize)
    };
}