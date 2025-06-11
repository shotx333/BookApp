using DataAccessLayer.Models.DatabaseModels;

namespace DataAccessLayer.Repositories.Interfaces;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAuthorsAsync();
    Task<Author> GetAuthorAsync(int id);
    Task AddAuthorAsync(Author author);
    Task UpdateAuthorAsync(int id, Author author);
    Task DeleteAuthorAsync(int id);
}