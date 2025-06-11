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

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _context.Books.Include(u => u.Author).ToListAsync();
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        return await _context.Books.Include(u => u.Author)
            .FirstOrDefaultAsync(u => u.Id == id) ?? throw new InvalidOperationException();
    }

    public async Task InsertAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, Book book)
    {
        var existingBook = await _context.Books.FindAsync(id) ??
                           throw new ArgumentException("Book with the provided id could not be found", nameof(id));


        _context.Entry(existingBook).CurrentValues.SetValues(book);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var book = await GetByIdAsync(id);
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }
}