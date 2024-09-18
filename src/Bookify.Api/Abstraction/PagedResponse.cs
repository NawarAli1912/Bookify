using Bookify.Domain.Abstraction;

namespace Bookify.Api.Abstraction;

public class PagedResponse<T>(
    IEnumerable<T> items,
    int count)
{
    public IEnumerable<T> Items { get; init; } = items;

    public int Count { get; init; } = count;

    public static PagedResponse<TResult> FromPagedResult<TResult>(PagedResult<TResult> result)
    {
        return new PagedResponse<TResult>(result.Items, result.Count);
    }
}
