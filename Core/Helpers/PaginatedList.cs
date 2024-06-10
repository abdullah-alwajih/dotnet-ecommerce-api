namespace Core.Helpers;

public class PaginatedList<T>
{
    public PaginatedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        Metadata = new PaginationMetadata
        {
            TotalCount = count,
            PageSize = pageSize,
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize)
        };

        Items = items;
    }

    public IEnumerable<T> Items { get; set; }
    public PaginationMetadata Metadata { get; set; }
}