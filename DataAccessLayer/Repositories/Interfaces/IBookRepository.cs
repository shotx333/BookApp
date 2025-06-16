using DataAccessLayer.Models.DatabaseModels;

namespace DataAccessLayer.Repositories.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book> GetByIdAsync(int id);
    Task<Book?> FindByIdAsync(int id);
    Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(int authorId);
    Task<Book?> FindByTitleAndAuthorAsync(string title, int authorId);
    Task<IEnumerable<Book>> SearchBooksAsync(string? title, string? genre, int? authorId);
    Task<Dictionary<string, int>> GetBookCountByGenreAsync();
    Task<int> GetTotalBookCountAsync();
    Task InsertAsync(Book book);
    Task UpdateAsync(int id, Book book);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}