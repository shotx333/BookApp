using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        try
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        try
        {
            var book = await _bookService.GetBookByIdAsync(id);
            return Ok(book);
        }
        catch (BookNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("author/{authorId}")]
    public async Task<IActionResult> GetBooksByAuthor(int authorId)
    {
        try
        {
            var books = await _bookService.GetBooksByAuthorAsync(authorId);
            return Ok(books);
        }
        catch (AuthorNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchBooks([FromQuery] string? title, [FromQuery] string? genre, [FromQuery] int? authorId)
    {
        try
        {
            var criteria = new BookSearchCriteria
            {
                Title = title,
                Genre = genre,
                AuthorId = authorId
            };

            var books = await _bookService.SearchBooksAsync(criteria);
            return Ok(books);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("statistics")]
    public async Task<IActionResult> GetBookStatistics()
    {
        try
        {
            var statistics = await _bookService.GetBookStatisticsAsync();
            return Ok(statistics);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] BookDto bookDto)
    {
        try
        {
            var book = await _bookService.CreateBookAsync(bookDto.Title, bookDto.Genre, bookDto.AuthorId);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }
        catch (InvalidBookDataException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DuplicateBookException ex)
        {
            return Conflict(ex.Message);
        }
        catch (AuthorNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDto bookDto)
    {
        try
        {
            var book = await _bookService.UpdateBookAsync(id, bookDto.Title, bookDto.Genre, bookDto.AuthorId);
            return Ok(book);
        }
        catch (BookNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidBookDataException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DuplicateBookException ex)
        {
            return Conflict(ex.Message);
        }
        catch (AuthorNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }
        catch (BookNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}