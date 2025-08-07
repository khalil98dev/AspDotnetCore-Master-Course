using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Response;

public class PagedResult<T>
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public bool HasNextPage => CurrentPage < TotalPages;    
    public bool HasPreviousPage => CurrentPage > 1;

    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);  
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();


    private PagedResult() { }   
   
    static public PagedResult<T> Create(IEnumerable<T> items, int currentPage, int pageSize, int totalCount)
    {

        return new PagedResult<T>

        {
            CurrentPage = currentPage,
            PageSize = pageSize,
            TotalCount = totalCount,
            Items = items
        };
    }
    
    

}