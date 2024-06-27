namespace Core.Interfaces;

public record ResultList<T>(IEnumerable<T> Items) : IResultList<T>
{
    public IEnumerable<T> Items { get; private set; } = Items;
}