using DataAccessLayer.Models.DatabaseModels;
using DataAccessLayer.Repositories.Interfaces;

namespace DataAccessLayer.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryDbContext _context;

    public AuthorRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public IQueryable<Author> GetAuthors()
    {
        return _context.Authors;
    }

    public Author GetAuthor(int id)
    {
        return _context.Authors.FirstOrDefault(x => x.Id == id) ?? throw new InvalidOperationException();
    }

    public void AddAuthor(Author author)
    {
        _context.Authors.Add(author);
        _context.SaveChanges();
    }

    public void UpdateAuthor(int id, Author author)
    {
        var existingAuthor = _context.Authors.Find(id);
        if (existingAuthor == null) return;

        _context.Entry(existingAuthor).CurrentValues.SetValues(author);
        _context.SaveChanges();
    }

    public void DeleteAuthor(int id)
    {
        var authorToDelete = _context.Authors.Find(id);
        if (authorToDelete == null) return;

        _context.Authors.Remove(authorToDelete);
        _context.SaveChanges();
    }
}