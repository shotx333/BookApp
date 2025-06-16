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

    public async Task<Book?> FindByIdAsync(int id)
    {
        return await _context.Books.Include(u => u.Author)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(int authorId)
    {
        return await _context.Books.Include(u => u.Author)
            .Where(b => b.AuthorId == authorId)
            .ToListAsync();
    }

    public async Task<Book?> FindByTitleAndAuthorAsync(string title, int authorId)
    {
        return await _context.Books
            .FirstOrDefaultAsync(b => b.Title.ToLower() == title.ToLower() && b.AuthorId == authorId);
    }

    public async Task<IEnumerable<Book>> SearchBooksAsync(string? title, string? genre, int? authorId)
    {
        var query = _context.Books.Include(b => b.Author).AsQueryable();

        if (!string.IsNullOrWhiteSpace(title))
        {
            query = query.Where(b => b.Title.ToLower().Contains(title.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(genre))
        {
            query = query.Where(b => b.Genre.ToLower().Contains(genre.ToLower()));
        }

        if (authorId.HasValue)
        {
            query = query.Where(b => b.AuthorId == authorId.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<Dictionary<string, int>> GetBookCountByGenreAsync()
    {
        return await _context.Books
            .GroupBy(b => b.Genre)
            .ToDictionaryAsync(g => g.Key, g => g.Count());
    }

    public async Task<int> GetTotalBookCountAsync()
    {
        return await _context.Books.CountAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Books.AnyAsync(b => b.Id == id);
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