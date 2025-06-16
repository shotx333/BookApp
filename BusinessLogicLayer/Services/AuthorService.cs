using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models.DatabaseModels;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        return await _authorRepository.GetAuthorsAsync();
    }

    public async Task<Author> GetAuthorByIdAsync(int id)
    {
        var author = await _authorRepository.FindAuthorAsync(id);
        if (author == null)
        {
            throw new AuthorNotFoundException(id);
        }
        return author;
    }

    public async Task<Author> CreateAuthorAsync(string firstName, string lastName)
    {
        // Validate input data
        await ValidateAuthorDataAsync(firstName, lastName);

        var author = new Author
        {
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            Books = new List<Book>()
        };

        await _authorRepository.AddAuthorAsync(author);
        return await GetAuthorByIdAsync(author.Id);
    }

    public async Task<Author> UpdateAuthorAsync(int id, string firstName, string lastName)
    {
        // Check if author exists
        var existingAuthor = await _authorRepository.FindAuthorAsync(id);
        if (existingAuthor == null)
        {
            throw new AuthorNotFoundException(id);
        }

        // Validate input data
        await ValidateAuthorDataAsync(firstName, lastName);

        var updatedAuthor = new Author
        {
            Id = id,
            FirstName = firstName.Trim(),
            LastName = lastName.Trim()
        };

        await _authorRepository.UpdateAuthorAsync(id, updatedAuthor);
        return await GetAuthorByIdAsync(id);
    }

    public async Task DeleteAuthorAsync(int id)
    {
        // Check if author exists
        if (!await _authorRepository.AuthorExistsAsync(id))
        {
            throw new AuthorNotFoundException(id);
        }

        // Validate that author can be deleted (no associated books)
        await ValidateAuthorCanBeDeletedAsync(id);

        await _authorRepository.DeleteAuthorAsync(id);
    }

    public async Task<IEnumerable<Author>> GetAuthorsWithBooksAsync()
    {
        return await _authorRepository.GetAuthorsWithBookCountAsync();
    }

    public async Task<Author> GetAuthorWithBooksAsync(int id)
    {
        var author = await _authorRepository.FindAuthorAsync(id);
        if (author == null)
        {
            throw new AuthorNotFoundException(id);
        }
        return author;
    }

    public async Task<int> GetAuthorBookCountAsync(int id)
    {
        // Check if author exists
        if (!await _authorRepository.AuthorExistsAsync(id))
        {
            throw new AuthorNotFoundException(id);
        }

        return await _authorRepository.GetAuthorBookCountAsync(id);
    }

    public async Task ValidateAuthorCanBeDeletedAsync(int id)
    {
        var bookCount = await _authorRepository.GetAuthorBookCountAsync(id);
        if (bookCount > 0)
        {
            throw new AuthorHasBooksException(id, bookCount);
        }
    }

    public async Task<bool> AuthorExistsAsync(int id)
    {
        return await _authorRepository.AuthorExistsAsync(id);
    }

    public async Task ValidateAuthorDataAsync(string firstName, string lastName)
    {
        var errors = new List<string>();

        // Validate first name
        if (string.IsNullOrWhiteSpace(firstName))
        {
            errors.Add("First name is required.");
        }
        else if (firstName.Trim().Length < 2)
        {
            errors.Add("First name must be at least 2 characters long.");
        }
        else if (firstName.Trim().Length > 50)
        {
            errors.Add("First name cannot exceed 50 characters.");
        }

        // Validate last name
        if (string.IsNullOrWhiteSpace(lastName))
        {
            errors.Add("Last name is required.");
        }
        else if (lastName.Trim().Length < 2)
        {
            errors.Add("Last name must be at least 2 characters long.");
        }
        else if (lastName.Trim().Length > 50)
        {
            errors.Add("Last name cannot exceed 50 characters.");
        }

        if (errors.Any())
        {
            throw new InvalidBookDataException(string.Join(" ", errors));
        }
    }
}