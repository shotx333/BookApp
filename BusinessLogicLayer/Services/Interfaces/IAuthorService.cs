using DataAccessLayer.Models.DatabaseModels;

namespace BusinessLogicLayer.Services.Interfaces;

public interface IAuthorService
{
    // Basic CRUD operations with business logic
    Task<IEnumerable<Author>> GetAllAuthorsAsync();
    Task<Author> GetAuthorByIdAsync(int id);
    Task<Author> CreateAuthorAsync(string firstName, string lastName);
    Task<Author> UpdateAuthorAsync(int id, string firstName, string lastName);
    Task DeleteAuthorAsync(int id);
    
    // Business logic methods
    Task<IEnumerable<Author>> GetAuthorsWithBooksAsync();
    Task<Author> GetAuthorWithBooksAsync(int id);
    Task<int> GetAuthorBookCountAsync(int id);
    Task ValidateAuthorCanBeDeletedAsync(int id);
    Task<bool> AuthorExistsAsync(int id);
    Task ValidateAuthorDataAsync(string firstName, string lastName);
}