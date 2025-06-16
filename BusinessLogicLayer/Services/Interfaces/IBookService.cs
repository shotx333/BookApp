using BusinessLogicLayer.Models;
using DataAccessLayer.Models.DatabaseModels;

namespace BusinessLogicLayer.Services.Interfaces;

public interface IBookService
{
    // Basic CRUD operations with business logic
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book> GetBookByIdAsync(int id);
    Task<Book> CreateBookAsync(string title, string genre, int authorId);
    Task<Book> UpdateBookAsync(int id, string title, string genre, int? authorId = null);
    Task DeleteBookAsync(int id);
    
    // Business logic methods
    Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId);
    Task<IEnumerable<Book>> SearchBooksAsync(BookSearchCriteria criteria);
    Task<BookStatistics> GetBookStatisticsAsync();
    Task<bool> IsBookTitleUniqueForAuthorAsync(string title, int authorId, int? excludeBookId = null);
    Task ValidateBookDataAsync(string title, string genre, int authorId);
}