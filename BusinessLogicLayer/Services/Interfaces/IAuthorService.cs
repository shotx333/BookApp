using DataAccessLayer.Models.DatabaseModels;

namespace BusinessLogicLayer.Services.Interfaces;

public interface IAuthorService
{
    IQueryable<DataAccessLayer.Models.DatabaseModels.Author> GetAuthors();
    DataAccessLayer.Models.DatabaseModels.Author GetAuthorByBookId(int bookId);
    void AddAuthor(DataAccessLayer.Models.DatabaseModels.Author author);
    void UpdateAuthor(int id, DataAccessLayer.Models.DatabaseModels.Author author);
    void DeleteAuthor(int id);
}