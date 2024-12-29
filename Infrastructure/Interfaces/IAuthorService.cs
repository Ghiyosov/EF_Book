using Domain.DTOs;
using Domain.Filters;
using Infrastructure.ApiResponses;

namespace Infrastructure.Interfaces;

public interface IAuthorService
{
    public Task<Responce<List<ReadAutorDTO>>> ReadAutors(AuthorFilter filter);
    public Task<Responce<ReadAutorDTO>> ReadAutor(int id);
    public Task<Responce<string>> CreateAuthor(CreateAuthorDTO author);
    public Task<Responce<string>> UpdateAuthor(UpdateAuthorDTO author);
    public Task<Responce<string>> DeleteAuthor(int id);
}