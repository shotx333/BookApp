using DataAccessLayer.Models.DatabaseModels;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryDbContext _context;

    public AuthorRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Author>> GetAuthorsAsync()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<Author> GetAuthorAsync(int id)
    {
        return await _context.Authors.FirstOrDefaultAsync(x => x.Id == id) ?? throw new InvalidOperationException();
    }

    public async Task<Author?> FindAuthorAsync(int id)
    {
        return await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Author>> GetAuthorsWithBookCountAsync()
    {
        return await _context.Authors.Include(a => a.Books).ToListAsync();
    }

    public async Task<bool> AuthorExistsAsync(int id)
    {
        return await _context.Authors.AnyAsync(a => a.Id == id);
    }

    public async Task<bool> AuthorHasBooksAsync(int id)
    {
        return await _context.Books.AnyAsync(b => b.AuthorId == id);
    }

    public async Task<int> GetAuthorBookCountAsync(int id)
    {
        return await _context.Books.CountAsync(b => b.AuthorId == id);
    }

    public async Task AddAuthorAsync(Author author)
    {
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAuthorAsync(int id, Author author)
    {
        var existingAuthor = await _context.Authors.FindAsync(id);
        if (existingAuthor == null) return;

        _context.Entry(existingAuthor).CurrentValues.SetValues(author);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAuthorAsync(int id)
    {
        var authorToDelete = await _context.Authors.FindAsync(id);
        if (authorToDelete == null) return;

        _context.Authors.Remove(authorToDelete);
        await _context.SaveChangesAsync();
    }
}