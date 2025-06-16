namespace BusinessLogicLayer.Models;

public class BookStatistics
{
    public int TotalBooks { get; set; }
    public int TotalAuthors { get; set; }
    public Dictionary<string, int> BooksByGenre { get; set; } = new();
    public List<AuthorBookCount> TopAuthors { get; set; } = new();
}

public class AuthorBookCount
{
    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public int BookCount { get; set; }
}