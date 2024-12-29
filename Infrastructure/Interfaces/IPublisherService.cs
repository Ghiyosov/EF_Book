using Domain.DTOs;
using Domain.Filters;
using Infrastructure.ApiResponses;

namespace Infrastructure.Interfaces;

public interface IPublisherService
{
    public Task<Responce<List<ReadPublisherDTO>>> ReadPublishers(PublisherFilter filter);
    public Task<Responce<ReadPublisherDTO>> ReadPublisher(int id);
    public Task<Responce<string>> CreatePublisher(CreatePublisherDTO publisher);
    public Task<Responce<string>> UpdatePublisher(UpdatePublisherDTO publisher);
    public Task<Responce<string>> DeletePublisher(int id);
}