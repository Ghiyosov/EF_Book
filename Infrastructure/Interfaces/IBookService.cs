using Domain.DTOs;
using Domain.Filters;
using Infrastructure.ApiResponses;

namespace Infrastructure.Interfaces;

public interface IBookService
{
    public Task<Responce<List<ReadBookDTO>>> ReadBooks(BookFilter filter);
    public Task<Responce<ReadBookDTO>> ReadBook(int id);
    public Task<Responce<string>> CreateBook(CreateBookDTO book);
    public Task<Responce<string>> UpdateBook(UpdateBookDTO book);
    public Task<Responce<string>> DeleteBook(int id);
}