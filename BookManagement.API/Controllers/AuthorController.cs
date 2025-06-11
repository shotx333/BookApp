using DataAccessLayer.Models.DatabaseModels;
using DataAccessLayer.Models.DTO;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorController(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var authors = await _authorRepository.GetAuthorsAsync();
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var author = await _authorRepository.GetAuthorAsync(id);
        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AuthorDto authorDto)
    {
        var author = new Author
        {
            FirstName = authorDto.FirstName,
            LastName = authorDto.LastName,
            Books = authorDto.Books.Select(b => new Book
            {
                Title = b.Title,
                Genre = b.Genre,
            }).ToList()
        };

        await _authorRepository.AddAuthorAsync(author);
        return CreatedAtAction(nameof(Get),  author);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] AuthorDto authorDto)
    {
        var author = new Author
        {
            Id = id,
            FirstName = authorDto.FirstName,
            LastName = authorDto.LastName
        };

        if (id != author.Id)
        {
            return BadRequest();
        }
        await _authorRepository.UpdateAuthorAsync(id, author);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _authorRepository.DeleteAuthorAsync(id);
        return NoContent();
    }
}