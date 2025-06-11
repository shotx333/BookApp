namespace BusinessLogicLayer.Services.Interfaces;

public interface IBookService
{
    Task<IEnumerable<DataAccessLayer.Models.DatabaseModels.Book>> GetAllBooksAsync();
    Task<DataAccessLayer.Models.DatabaseModels.Book> GetBookByIdAsync(int id);
    Task<DataAccessLayer.Models.DatabaseModels.Book> AddBookAsync(DataAccessLayer.Models.DatabaseModels.Book book);
    Task UpdateBookAsync(int id, DataAccessLayer.Models.DatabaseModels.Book book);
    Task DeleteBookAsync(int id);
}