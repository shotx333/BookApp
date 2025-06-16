namespace BusinessLogicLayer.Models;

public class BookSearchCriteria
{
    public string? Title { get; set; }
    public string? Genre { get; set; }
    public int? AuthorId { get; set; }
    public string? AuthorName { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}