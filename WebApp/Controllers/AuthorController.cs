using Domain.DTOs;
using Domain.Filters;
using Infrastructure.ApiResponses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthorController(IAuthorService _service)
{
    [HttpGet("RedAuthor")]
    public async Task<Responce<List<ReadAutorDTO>>> GetAuthors([FromQuery]AuthorFilter filter)
        => await _service.ReadAutors(filter);

    [HttpGet("RedAuthor/{id}")]
    public async Task<Responce<ReadAutorDTO>> GetAuthorById(int id)
        => await _service.ReadAutor(id);
    
    [HttpPost("CreateAuthor")]
    public async Task<Responce<string>> CreateAuthor(CreateAuthorDTO author) 
        => await _service.CreateAuthor(author);
    
    [HttpPut("UpdateAuthor")]
    public async Task<Responce<string>> UpdateAuthor(UpdateAuthorDTO author) 
        => await _service.UpdateAuthor(author);
    
    [HttpDelete("DeleteAuthor/{id}")]
    public async Task<Responce<string>> DeleteAuthor(int id) 
        => await _service.DeleteAuthor(id);
}