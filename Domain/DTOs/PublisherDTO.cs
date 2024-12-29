using Domain.Entities;

namespace Domain.DTOs;

public class PublisherDTO
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string ContactEmail { get; set; }
    public int EstablishedYear { get; set; }
    public string Website { get; set; }
}

public class CreatePublisherDTO : PublisherDTO;

public class UpdatePublisherDTO : CreatePublisherDTO
{
    public int Id { get; set; }
}

public class ReadPublisherDTO : UpdatePublisherDTO
{
    public List<UpdateBookDTO> Books { get; set; }
}