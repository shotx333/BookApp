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

    public async Task<IEnumerable<DataAccessLayer.Models.DatabaseModels.Author>> GetAuthorsAsync()
    {
        return await _authorRepository.GetAuthorsAsync();
    }

    public async Task<DataAccessLayer.Models.DatabaseModels.Author> GetAuthorByBookIdAsync(int id)
    {
        return await _authorRepository.GetAuthorAsync(id);
    }

    public async Task AddAuthorAsync(DataAccessLayer.Models.DatabaseModels.Author author)
    {
        await _authorRepository.AddAuthorAsync(author);
    }

    public async Task UpdateAuthorAsync(int id, DataAccessLayer.Models.DatabaseModels.Author author)
    {
        await _authorRepository.UpdateAuthorAsync(id, author);
    }

    public async Task DeleteAuthorAsync(int id)
    {
        await _authorRepository.DeleteAuthorAsync(id);
    }
}