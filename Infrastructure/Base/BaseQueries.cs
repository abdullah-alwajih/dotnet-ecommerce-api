namespace Core.Interfaces;





public record BaseQueries(string? Name = null, decimal? Price = null) : Pagination;
