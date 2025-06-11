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
    public async Task<IActionResult> Get()
    {
        var books = await _bookRepository.GetAllAsync();
        return Ok(books);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        return Ok(book);
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BookDto bookDto)
    {

        var book = new Book
        {
            Title = bookDto.Title,
            Genre = bookDto.Genre,
            AuthorId = bookDto.AuthorId
        };
        await _bookRepository.InsertAsync(book);
        return CreatedAtAction(nameof(Get),  book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] BookDto bookDto)
    {
       var book= await _bookRepository.GetByIdAsync(id);

       book.Title = bookDto.Title;
       book.Genre = bookDto.Genre;

        if (id != book.Id)
        {
            return BadRequest();
        }
        await _bookRepository.UpdateAsync(id, book);
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _bookRepository.DeleteAsync(id);
        return NoContent();
    }
}