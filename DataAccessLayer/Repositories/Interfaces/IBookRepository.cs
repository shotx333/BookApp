using DataAccessLayer.Models.DatabaseModels;

namespace DataAccessLayer.Repositories.Interfaces;

public interface IBookRepository
{
    IEnumerable<Book> GetAll();
    Book GetById(int id);
    void Insert(Book book);
    void Update(int id, Book book);
    void Delete(int id);
}