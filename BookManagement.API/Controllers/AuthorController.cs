using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        try
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthor(int id)
    {
        try
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            return Ok(author);
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

    [HttpGet("{id}/with-books")]
    public async Task<IActionResult> GetAuthorWithBooks(int id)
    {
        try
        {
            var author = await _authorService.GetAuthorWithBooksAsync(id);
            return Ok(author);
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

    [HttpGet("with-books")]
    public async Task<IActionResult> GetAuthorsWithBooks()
    {
        try
        {
            var authors = await _authorService.GetAuthorsWithBooksAsync();
            return Ok(authors);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}/book-count")]
    public async Task<IActionResult> GetAuthorBookCount(int id)
    {
        try
        {
            var bookCount = await _authorService.GetAuthorBookCountAsync(id);
            return Ok(new { AuthorId = id, BookCount = bookCount });
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

    [HttpPost]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorDto authorDto)
    {
        try
        {
            var author = await _authorService.CreateAuthorAsync(authorDto.FirstName, authorDto.LastName);
            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }
        catch (InvalidBookDataException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorDto authorDto)
    {
        try
        {
            var author = await _authorService.UpdateAuthorAsync(id, authorDto.FirstName, authorDto.LastName);
            return Ok(author);
        }
        catch (AuthorNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidBookDataException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        try
        {
            await _authorService.DeleteAuthorAsync(id);
            return NoContent();
        }
        catch (AuthorNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (AuthorHasBooksException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}