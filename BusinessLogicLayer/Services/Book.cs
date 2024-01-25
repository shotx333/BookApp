using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services;

public class Book : IBookService
{
    private readonly IBookRepository _bookRepository;

    public Book(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public IEnumerable<DataAccessLayer.Models.DatabaseModels.Book> GetAllBooks()
    {
        return _bookRepository.GetAll();
    }

    public DataAccessLayer.Models.DatabaseModels.Book GetBookById(int id)
    {
        return _bookRepository.GetById(id);
    }

    public DataAccessLayer.Models.DatabaseModels.Book AddBook(DataAccessLayer.Models.DatabaseModels.Book book)
    {
        _bookRepository.Insert(book);
        return book;
    }

    public void UpdateBook(int id, DataAccessLayer.Models.DatabaseModels.Book book)
    {
        _bookRepository.Update(id, book);
    }

    public void DeleteBook(int id)
    {
        _bookRepository.Delete(id);
    }


}