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

    public async Task<IEnumerable<DataAccessLayer.Models.DatabaseModels.Book>> GetAllBooksAsync()
    {
        return await _bookRepository.GetAllAsync();
    }

    public async Task<DataAccessLayer.Models.DatabaseModels.Book> GetBookByIdAsync(int id)
    {
        return await _bookRepository.GetByIdAsync(id);
    }

    public async Task<DataAccessLayer.Models.DatabaseModels.Book> AddBookAsync(DataAccessLayer.Models.DatabaseModels.Book book)
    {
        await _bookRepository.InsertAsync(book);
        return book;
    }

    public async Task UpdateBookAsync(int id, DataAccessLayer.Models.DatabaseModels.Book book)
    {
        await _bookRepository.UpdateAsync(id, book);
    }

    public async Task DeleteBookAsync(int id)
    {
        await _bookRepository.DeleteAsync(id);
    }


}