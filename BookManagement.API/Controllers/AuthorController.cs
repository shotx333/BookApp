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
    public IActionResult Get()
    {
        var authors = _authorRepository.GetAuthors();
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var author = _authorRepository.GetAuthor(id);
        return Ok(author);
    }

    [HttpPost]
    public IActionResult Post([FromBody] AuthorDto authorDto)
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
                
        _authorRepository.AddAuthor(author);
        return CreatedAtAction(nameof(Get),  author);
    }


    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] AuthorDto authorDto)
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
        _authorRepository.UpdateAuthor(id, author);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _authorRepository.DeleteAuthor(id);
        return NoContent();
    }
}