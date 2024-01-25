using DataAccessLayer.Models.DatabaseModels;
using DataAccessLayer.Models.DTO;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var books = _bookRepository.GetAll();
        return Ok(books);
    }


    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var book = _bookRepository.GetById(id);
        return Ok(book);
    }


    [HttpPost]
    public IActionResult Post([FromBody] BookDto bookDto)
    {

        var book = new Book
        {
            Title = bookDto.Title,
            Genre = bookDto.Genre,
            AuthorId = bookDto.AuthorId
        };
        _bookRepository.Insert(book);
        return CreatedAtAction(nameof(Get),  book);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] BookDto bookDto)
    {
       var book= _bookRepository.GetById(id);
    
       book.Title = bookDto.Title;
       book.Genre = bookDto.Genre;

        if (id != book.Id)
        {
            return BadRequest();
        }
        _bookRepository.Update(id, book);
        return NoContent();
    }


    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _bookRepository.Delete(id);
        return NoContent();
    }
}