namespace BusinessLogicLayer.Services.Interfaces;

public interface IBookService
{
    IEnumerable<DataAccessLayer.Models.DatabaseModels.Book> GetAllBooks();
    DataAccessLayer.Models.DatabaseModels.Book GetBookById(int id);
    DataAccessLayer.Models.DatabaseModels.Book AddBook(DataAccessLayer.Models.DatabaseModels.Book book);
    void UpdateBook(int id, DataAccessLayer.Models.DatabaseModels.Book book);
    void DeleteBook(int id);
}