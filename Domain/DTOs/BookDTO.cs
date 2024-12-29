using Domain.Entities;

namespace Domain.DTOs;

public class BookDTO
{
    public string Title { get; set; }
    public DateTime PublicationDate { get; set; }
    public string Genre { get; set; }
    public int Pages { get; set; }
    public string Language { get; set; }
    public int AuthorId { get; set; }
    public int PublisherId { get; set; }
}

public class CreateBookDTO : BookDTO;

public class UpdateBookDTO : CreateBookDTO
{
    public int Id { get; set; }
}

public class ReadBookDTO : UpdateBookDTO
{
    public UpdateAuthorDTO Author { get; set; }
    public UpdatePublisherDTO Publisher  { get; set; }
}