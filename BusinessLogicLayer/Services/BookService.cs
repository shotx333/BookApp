using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models.DatabaseModels;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;

    public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _bookRepository.GetAllAsync();
    }

    public async Task<Book> GetBookByIdAsync(int id)
    {
        var book = await _bookRepository.FindByIdAsync(id);
        if (book == null)
        {
            throw new BookNotFoundException(id);
        }
        return book;
    }

    public async Task<Book> CreateBookAsync(string title, string genre, int authorId)
    {
        // Validate input data
        await ValidateBookDataAsync(title, genre, authorId);

        // Check for duplicate title for the same author
        if (!await IsBookTitleUniqueForAuthorAsync(title, authorId))
        {
            throw new DuplicateBookException(title, authorId);
        }

        var book = new Book
        {
            Title = title.Trim(),
            Genre = genre.Trim(),
            AuthorId = authorId
        };

        await _bookRepository.InsertAsync(book);
        return await GetBookByIdAsync(book.Id);
    }

    public async Task<Book> UpdateBookAsync(int id, string title, string genre, int? authorId = null)
    {
        // Check if book exists
        var existingBook = await _bookRepository.FindByIdAsync(id);
        if (existingBook == null)
        {
            throw new BookNotFoundException(id);
        }

        // Use existing authorId if not provided
        var targetAuthorId = authorId ?? existingBook.AuthorId;

        // Validate input data
        await ValidateBookDataAsync(title, genre, targetAuthorId);

        // Check for duplicate title for the same author (excluding current book)
        if (!await IsBookTitleUniqueForAuthorAsync(title, targetAuthorId, id))
        {
            throw new DuplicateBookException(title, targetAuthorId);
        }

        var updatedBook = new Book
        {
            Id = id,
            Title = title.Trim(),
            Genre = genre.Trim(),
            AuthorId = targetAuthorId
        };

        await _bookRepository.UpdateAsync(id, updatedBook);
        return await GetBookByIdAsync(id);
    }

    public async Task DeleteBookAsync(int id)
    {
        // Check if book exists
        if (!await _bookRepository.ExistsAsync(id))
        {
            throw new BookNotFoundException(id);
        }

        await _bookRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId)
    {
        // Validate author exists
        if (!await _authorRepository.AuthorExistsAsync(authorId))
        {
            throw new AuthorNotFoundException(authorId);
        }

        return await _bookRepository.GetBooksByAuthorIdAsync(authorId);
    }

    public async Task<IEnumerable<Book>> SearchBooksAsync(BookSearchCriteria criteria)
    {
        if (criteria == null)
        {
            return await GetAllBooksAsync();
        }

        return await _bookRepository.SearchBooksAsync(criteria.Title, criteria.Genre, criteria.AuthorId);
    }

    public async Task<BookStatistics> GetBookStatisticsAsync()
    {
        var totalBooks = await _bookRepository.GetTotalBookCountAsync();
        var booksByGenre = await _bookRepository.GetBookCountByGenreAsync();
        var authorsWithBooks = await _authorRepository.GetAuthorsWithBookCountAsync();

        var topAuthors = authorsWithBooks
            .Where(a => a.Books.Any())
            .Select(a => new AuthorBookCount
            {
                AuthorId = a.Id,
                AuthorName = $"{a.FirstName} {a.LastName}",
                BookCount = a.Books.Count
            })
            .OrderByDescending(a => a.BookCount)
            .Take(10)
            .ToList();

        return new BookStatistics
        {
            TotalBooks = totalBooks,
            TotalAuthors = authorsWithBooks.Count(),
            BooksByGenre = booksByGenre,
            TopAuthors = topAuthors
        };
    }

    public async Task<bool> IsBookTitleUniqueForAuthorAsync(string title, int authorId, int? excludeBookId = null)
    {
        var existingBook = await _bookRepository.FindByTitleAndAuthorAsync(title, authorId);
        
        if (existingBook == null)
        {
            return true;
        }

        // If we're updating a book, exclude it from the uniqueness check
        return excludeBookId.HasValue && existingBook.Id == excludeBookId.Value;
    }

    public async Task ValidateBookDataAsync(string title, string genre, int authorId)
    {
        var errors = new List<string>();

        // Validate title
        if (string.IsNullOrWhiteSpace(title))
        {
            errors.Add("Title is required.");
        }
        else if (title.Trim().Length < 2)
        {
            errors.Add("Title must be at least 2 characters long.");
        }
        else if (title.Trim().Length > 200)
        {
            errors.Add("Title cannot exceed 200 characters.");
        }

        // Validate genre
        if (string.IsNullOrWhiteSpace(genre))
        {
            errors.Add("Genre is required.");
        }
        else if (genre.Trim().Length < 2)
        {
            errors.Add("Genre must be at least 2 characters long.");
        }
        else if (genre.Trim().Length > 50)
        {
            errors.Add("Genre cannot exceed 50 characters.");
        }

        // Validate author exists
        if (authorId <= 0)
        {
            errors.Add("Author ID must be a positive number.");
        }
        else if (!await _authorRepository.AuthorExistsAsync(authorId))
        {
            errors.Add($"Author with ID {authorId} does not exist.");
        }

        if (errors.Any())
        {
            throw new InvalidBookDataException(string.Join(" ", errors));
        }
    }
}