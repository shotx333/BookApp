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