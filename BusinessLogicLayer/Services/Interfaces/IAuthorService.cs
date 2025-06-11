using DataAccessLayer.Models.DatabaseModels;

namespace BusinessLogicLayer.Services.Interfaces;

public interface IAuthorService
{
    Task<IEnumerable<DataAccessLayer.Models.DatabaseModels.Author>> GetAuthorsAsync();
    Task<DataAccessLayer.Models.DatabaseModels.Author> GetAuthorByBookIdAsync(int bookId);
    Task AddAuthorAsync(DataAccessLayer.Models.DatabaseModels.Author author);
    Task UpdateAuthorAsync(int id, DataAccessLayer.Models.DatabaseModels.Author author);
    Task DeleteAuthorAsync(int id);
}