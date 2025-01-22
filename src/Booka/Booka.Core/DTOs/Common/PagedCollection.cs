using System.Collections;

namespace Booka.Core.DTOs.Common;

public class PagedCollection<T> : IReadOnlyCollection<T>
{
    public PagedCollection(List<T> items, int totalCount, int page, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        Page = page;
        PageSize = pageSize;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return Items.AsEnumerable().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count => Items.Count;

    public int TotalCount { get; }

    public int Page { get; }

    public int PageSize { get; }

    public List<T> Items { get; }
}