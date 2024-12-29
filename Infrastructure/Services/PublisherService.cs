using System.Net;
using Domain.DTOs;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.ApiResponses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class PublisherService(DataContext _data) : IPublisherService
{
    public async Task<Responce<List<ReadPublisherDTO>>> ReadPublishers(PublisherFilter filter)
    {
        var res =  _data.Publishers.Include(c=>c.Books).AsQueryable();
        if (filter != null)
            res = res.Where(c=>c.Name.ToLower().Contains(filter.Name.ToLower()));

        var publishers = res.Select(x => new ReadPublisherDTO()
        {
            Id = x.Id,
            Name = x.Name,
            Address = x.Address,
            ContactEmail = x.ContactEmail,
            EstablishedYear = x.EstablishedYear,
            Website = x.Website,
            Books = x.Books.Select(c => new UpdateBookDTO()
            {
                Id = c.Id,
                Title = c.Title,
                PublicationDate = c.PublicationDate,
                Genre = c.Genre,
                Pages = c.Pages,
                Language = c.Language
            }).ToList()
        }).ToList();

        return new Responce<List<ReadPublisherDTO>>(publishers);
    }

    public async Task<Responce<ReadPublisherDTO>> ReadPublisher(int id)
    {
        var x = await _data.Publishers.Include(c=>c.Books).FirstOrDefaultAsync(c => c.Id == id);
       
        if (x == null)
            return new Responce<ReadPublisherDTO>(HttpStatusCode.NotFound, "Publisher not found");
        
        var publisher = new ReadPublisherDTO()
        {
            Id = x.Id,
            Name = x.Name,
            Address = x.Address,
            ContactEmail = x.ContactEmail,
            EstablishedYear = x.EstablishedYear,
            Website = x.Website,
            Books = x.Books.Select(c => new UpdateBookDTO()
            {
                Id = c.Id,
                Title = c.Title,
                PublicationDate = c.PublicationDate,
                Genre = c.Genre,
                Pages = c.Pages,
                Language = c.Language
            }).ToList()
        };
        return new Responce<ReadPublisherDTO>(publisher);
    }

    public async Task<Responce<string>> CreatePublisher(CreatePublisherDTO publisher)
    {
        var pub = new Publisher()
        {
            Name = publisher.Name,
            Address = publisher.Address,
            ContactEmail = publisher.ContactEmail,
            EstablishedYear = publisher.EstablishedYear,
            Website = publisher.Website
        };
        _data.Publishers.Add(pub);
        var res = await _data.SaveChangesAsync();
        return res == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal server error")
            : new Responce<string>(HttpStatusCode.Created, "Publisher successfully created");
    }

    public async Task<Responce<string>> UpdatePublisher(UpdatePublisherDTO publisher)
    {
        var x = await _data.Publishers.Include(c=>c.Books).FirstOrDefaultAsync(c => c.Id == publisher.Id);
       
        if (x == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Publisher not found");
        
        x.Name = publisher.Name;
        x.Address = publisher.Address;
        x.ContactEmail = publisher.ContactEmail;
        x.EstablishedYear = publisher.EstablishedYear;
        x.Website = publisher.Website;
        
        var res = await _data.SaveChangesAsync();
        return res == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal server error")
            : new Responce<string>(HttpStatusCode.Created, "Publisher successfully updated");
    }

    public async Task<Responce<string>> DeletePublisher(int id)
    {
        var x = await _data.Publishers.Include(c=>c.Books).FirstOrDefaultAsync(c => c.Id == id);
       
        if (x == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Publisher not found");
        
         _data.Publishers.Remove(x);
        
         var res = await _data.SaveChangesAsync();
         return res == 0 
             ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal server error")
             : new Responce<string>(HttpStatusCode.Created, "Publisher successfully deleted");

    }
}