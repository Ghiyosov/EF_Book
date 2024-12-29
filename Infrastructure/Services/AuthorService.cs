using System.Net;
using Domain.DTOs;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.ApiResponses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AuthorService(DataContext _data) : IAuthorService
{
    public async Task<Responce<List<ReadAutorDTO>>> ReadAutors(AuthorFilter filter)
    {
        var res = _data.Authors
            .Include(c => c.Books).AsQueryable();
        if (filter.Name != null)
            res = res.Where(x=>x.Name.ToLower().Contains(filter.Name.ToLower()));
        
        if (filter.Nationality != null)
            res = res.Where(x => x.Nationality.ToLower().Contains(filter.Nationality.ToLower()));
        
        if (filter.Award != null)
            res = res.Where(x=>x.Awards.ToLower().Contains(filter.Award.ToLower()));

        var authors = res.Select(x => new ReadAutorDTO()
        {
            Id = x.Id,
            Name = x.Name,
            Biography = x.Biography,
            DateOfBirth = x.DateOfBirth,
            Nationality = x.Nationality,
            Awards = x.Awards,
            Books = x.Books.Select(b => new UpdateBookDTO()
            {
                Id = b.Id,
                Title = b.Title,
                PublicationDate = b.PublicationDate,
                Genre = b.Genre,
                Pages = b.Pages,
                Language = b.Language,
                PublisherId = b.PublisherId
            }).ToList()
        }).ToList();
        return new Responce<List<ReadAutorDTO>>(authors);
    }

    public async Task<Responce<ReadAutorDTO>> ReadAutor(int id)
    {
        var x = await _data.Authors.Include(c => c.Books).FirstOrDefaultAsync(x => x.Id == id);

        if (x == null)
            return new Responce<ReadAutorDTO>(HttpStatusCode.NotFound, "Author not found");
        
        var author = new ReadAutorDTO()
        {
            Id = x.Id,
            Name = x.Name,
            Biography = x.Biography,
            DateOfBirth = x.DateOfBirth,
            Nationality = x.Nationality,
            Awards = x.Awards,
            Books = x.Books.Select(b => new UpdateBookDTO()
            {
                Id = b.Id,
                Title = b.Title,
                PublicationDate = b.PublicationDate,
                Genre = b.Genre,
                Pages = b.Pages,
                Language = b.Language,
                PublisherId = b.PublisherId
            }).ToList()
        };
        
        return new Responce<ReadAutorDTO>(author);
    }

    public async Task<Responce<string>> CreateAuthor(CreateAuthorDTO author)
    {
        var au = new Author()
        {
            Name = author.Name,
            Biography = author.Biography,
            DateOfBirth = author.DateOfBirth,
            Nationality = author.Nationality,
            Awards = author.Awards
        };
        await _data.Authors.AddAsync(au);
        var result = await _data.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal server error")
            : new Responce<string>(HttpStatusCode.Created, "Author created");
    }

    public async Task<Responce<string>> UpdateAuthor(UpdateAuthorDTO author)
    {
        var au = await _data.Authors.FirstOrDefaultAsync(x => x.Id == author.Id);
        
        if (au == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Author not found");
        
        au.Name = author.Name;
        au.Biography = author.Biography;
        au.DateOfBirth = author.DateOfBirth;
        au.Nationality = author.Nationality;
        au.Awards = author.Awards;
        
        var result = await _data.SaveChangesAsync();
        return result == 0
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal server error")
            : new Responce<string>(HttpStatusCode.Created, "Author updated");
    }

    public async Task<Responce<string>> DeleteAuthor(int id)
    {
        var au = await _data.Authors.FirstOrDefaultAsync(x => x.Id == id);
        
        if (au == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Author not found");

        _data.Remove(au);
        var result = await _data.SaveChangesAsync();
        return result == 0
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal server error")
            : new Responce<string>(HttpStatusCode.Created, "Author deleted");

    }
}