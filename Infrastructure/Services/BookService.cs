using System.Net;
using Domain.DTOs;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.ApiResponses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class BookService(DataContext _data) : IBookService
{
    public async Task<Responce<List<ReadBookDTO>>> ReadBooks(BookFilter filter)
    {
        var res =
            _data.Books
                .Include(c => c.Author)
                .Include(z => z.Publisher)
                .AsQueryable();
        if (filter.Author != null)
            res = res.Where(x=>x.Author.Name.ToLower().Contains(filter.Author.ToLower()));
        
        if (filter.Publisher != null)
            res = res.Where(x => x.Publisher.Name.ToLower().Contains(filter.Publisher.ToLower()));
        
        if (filter.Title != null)
            res = res.Where(x => x.Title.ToLower().Contains(filter.Title.ToLower()));
        
        if (filter.Genre != null)
            res = res.Where(x => x.Title.ToLower().Contains(filter.Genre.ToLower()));
        
        if (filter.Language != null)
            res = res.Where(x => x.Language.ToLower().Contains(filter.Language.ToLower()));

        var books = res.Select(z => new ReadBookDTO()
        {
            Id = z.Id,
            Title = z.Title,
            PublicationDate = z.PublicationDate,
            Genre = z.Genre,
            Pages = z.Pages,
            Language = z.Language,
            AuthorId = z.AuthorId,
            PublisherId = z.PublisherId,
            Publisher = new UpdatePublisherDTO()
            {
                Id = z.Publisher.Id,
                Name = z.Publisher.Name,
                Address = z.Publisher.Address,
                ContactEmail = z.Publisher.ContactEmail,
                EstablishedYear = z.Publisher.EstablishedYear,
                Website = z.Publisher.Website
            },
            Author = new UpdateAuthorDTO()
            {
                Id = z.Author.Id,
                Name = z.Author.Name,
                Biography = z.Author.Biography,
                DateOfBirth = z.Author.DateOfBirth,
                Nationality = z.Author.Nationality,
                Awards = z.Author.Awards
            }
        }).ToList();
        
        return new Responce<List<ReadBookDTO>>(books);
    }

    public async Task<Responce<ReadBookDTO>> ReadBook(int id)
    {
        var z = await _data.Books.FirstOrDefaultAsync(c=>c.Id == id);
        
        if (z == null)
            return new Responce<ReadBookDTO>(HttpStatusCode.NotFound, "Book Not Found");

        var res = new ReadBookDTO()
        {
            Id = z.Id,
            Title = z.Title,
            PublicationDate = z.PublicationDate,
            Genre = z.Genre,
            Pages = z.Pages,
            Language = z.Language,
            AuthorId = z.AuthorId,
            PublisherId = z.PublisherId,
            Publisher = new UpdatePublisherDTO()
            {
                Id = z.Publisher.Id,
                Name = z.Publisher.Name,
                Address = z.Publisher.Address,
                ContactEmail = z.Publisher.ContactEmail,
                EstablishedYear = z.Publisher.EstablishedYear,
                Website = z.Publisher.Website
            },
            Author = new UpdateAuthorDTO()
            {
                Id = z.Author.Id,
                Name = z.Author.Name,
                Biography = z.Author.Biography,
                DateOfBirth = z.Author.DateOfBirth,
                Nationality = z.Author.Nationality,
                Awards = z.Author.Awards
            }
        };
        
        return new Responce<ReadBookDTO>(res);
    }

    public async Task<Responce<string>> CreateBook(CreateBookDTO book)
    {
        var bb = new Book()
        {
            Title = book.Title,
            PublicationDate = book.PublicationDate,
            Genre = book.Genre,
            Pages = book.Pages,
            Language = book.Language,
            AuthorId = book.AuthorId,
            PublisherId = book.PublisherId
        };
        
        await _data.Books.AddAsync(bb);
        var res = await _data.SaveChangesAsync();
        return res == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "Book Created");
    }

    public async Task<Responce<string>> UpdateBook(UpdateBookDTO book)
    {
        var bb = await _data.Books.FirstOrDefaultAsync(c => c.Id == book.Id);
        
        if (bb == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Book Not Found");
        bb.Title = book.Title;
        bb.PublicationDate = book.PublicationDate;
        bb.Genre = book.Genre;
        bb.Pages = book.Pages;
        bb.Language = book.Language;
        bb.AuthorId = book.AuthorId;
        bb.PublisherId = book.PublisherId;
        
        var res = await _data.SaveChangesAsync();
        return res == 0
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "Book Updated");
    }

    public async Task<Responce<string>> DeleteBook(int id)
    {
        var bb = await _data.Books.FirstOrDefaultAsync(c => c.Id == id);
        if (bb == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Book Not Found");
        
        _data.Books.Remove(bb);
        var res = await _data.SaveChangesAsync();
        return res == 0
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "Book Deleted");
    }
}