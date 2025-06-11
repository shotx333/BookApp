using DataAccessLayer.Models.DatabaseModels;

namespace DataAccessLayer.Repositories.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book> GetByIdAsync(int id);
    Task InsertAsync(Book book);
    Task UpdateAsync(int id, Book book);
    Task DeleteAsync(int id);
}