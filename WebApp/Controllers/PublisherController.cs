using Domain.DTOs;
using Domain.Filters;
using Infrastructure.ApiResponses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class PublisherController(IPublisherService _service)
{
    [HttpGet("RedPublishers")]
    public async Task<Responce<List<ReadPublisherDTO>>> RedPublishers([FromQuery]PublisherFilter filter)
        => await _service.ReadPublishers(filter);

    [HttpGet("ReadPublisher")]
    public async Task<Responce<ReadPublisherDTO>> ReadPublisher(int id)
        => await _service.ReadPublisher(id);
    
    [HttpPost("CreatePublisher")]
    public async Task<Responce<string>> CreatePublisher(CreatePublisherDTO publisher) 
        => await _service.CreatePublisher(publisher);
    
    [HttpPut("UpdatePublisher")]
    public async Task<Responce<string>> UpdatePublisher(UpdatePublisherDTO publisher)
        => await _service.UpdatePublisher(publisher);

    [HttpDelete("DeletePublisher")]
    public async Task<Responce<string>> DeletePublisher(int id)
        => await _service.DeletePublisher(id);
}