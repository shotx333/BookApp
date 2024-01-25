using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services;

public class Author : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public Author(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public IQueryable<DataAccessLayer.Models.DatabaseModels.Author> GetAuthors()
    {
        return _authorRepository.GetAuthors();
    }

    public DataAccessLayer.Models.DatabaseModels.Author GetAuthorByBookId(int id)
    {
        return _authorRepository.GetAuthor(id);
    }

    public void AddAuthor(DataAccessLayer.Models.DatabaseModels.Author author)
    {
        _authorRepository.AddAuthor(author);
    }

    public void UpdateAuthor(int id, DataAccessLayer.Models.DatabaseModels.Author author)
    {
        _authorRepository.UpdateAuthor(id, author);
    }

    public void DeleteAuthor(int id)
    {
        _authorRepository.DeleteAuthor(id);
    }
}