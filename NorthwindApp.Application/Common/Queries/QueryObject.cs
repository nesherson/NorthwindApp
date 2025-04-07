namespace NorthwindApp.Application;

public class QueryObject
{
    public string? SortBy { get; set; }
    public SortOrder SortOrder { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
} 
