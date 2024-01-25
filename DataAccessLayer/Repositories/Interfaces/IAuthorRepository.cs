using DataAccessLayer.Models.DatabaseModels;

namespace DataAccessLayer.Repositories.Interfaces;

public interface IAuthorRepository
{
    IQueryable<Author> GetAuthors();
    Author GetAuthor(int id);
    void AddAuthor(Author author);
    void UpdateAuthor(int id, Author author);
    void DeleteAuthor(int id);
}