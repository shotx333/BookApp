using DataAccessLayer.Models.DatabaseModels;

namespace DataAccessLayer.Repositories.Interfaces;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAuthorsAsync();
    Task<Author> GetAuthorAsync(int id);
    Task<Author?> FindAuthorAsync(int id);
    Task<IEnumerable<Author>> GetAuthorsWithBookCountAsync();
    Task<bool> AuthorExistsAsync(int id);
    Task<bool> AuthorHasBooksAsync(int id);
    Task<int> GetAuthorBookCountAsync(int id);
    Task AddAuthorAsync(Author author);
    Task UpdateAuthorAsync(int id, Author author);
    Task DeleteAuthorAsync(int id);
}