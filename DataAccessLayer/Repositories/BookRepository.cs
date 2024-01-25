using DataAccessLayer.Models.DatabaseModels;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;

    public BookRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Book> GetAll()
    {
        return _context.Books.Include(u => u.Author);
    }

    public Book GetById(int id)
    {
        return _context.Books.Include(u => u.Author).FirstOrDefault(u => u.Id == id) ?? throw new InvalidOperationException();
    }

    public void Insert(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public void Update(int id, Book book)
    {
        var existingBook = _context.Books.Find(id) ??
                           throw new ArgumentException("Book with the provided id could not be found", nameof(id));


        _context.Entry(existingBook).CurrentValues.SetValues(book);

        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var book = GetById(id);
        _context.Books.Remove(book);
        _context.SaveChanges();
    }
}