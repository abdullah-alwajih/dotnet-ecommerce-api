namespace Core.Interfaces;

public interface IResultList<out T>
{
    IEnumerable<T> Items { get; }
}