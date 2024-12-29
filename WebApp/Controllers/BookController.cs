using Domain.DTOs;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.ApiResponses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class BookController(IBookService _service)
{
    [HttpGet("GetBooks")]
    public async Task<Responce<List<ReadBookDTO>>> GetBooks([FromQuery]BookFilter filter)
        => await _service.ReadBooks(filter);
    
    [HttpGet("GetBook/{id}")]
    public async Task<Responce<ReadBookDTO>> GetBook(int id) 
        => await _service.ReadBook(id);
    
    [HttpPost("AddBook")]
    public async Task<Responce<string>> CreateBook(CreateBookDTO book)
        => await _service.CreateBook(book);
    
    [HttpPut("UpdateBook")]
    public async Task<Responce<string>> UpdateBook(UpdateBookDTO book) 
        => await _service.UpdateBook(book);
    
    [HttpDelete("DeleteBook/{id}")]
    public async Task<Responce<string>> DeleteBook(int id) 
        => await _service.DeleteBook(id);
}