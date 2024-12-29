using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Publisher
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50), Required]
    public string Name { get; set; }
    public string Address { get; set; }
    [EmailAddress]
    public string ContactEmail { get; set; }
    public int EstablishedYear { get; set; }
    [Url]
    public string Website { get; set; }

    public List<Book> Books { get; set; }
}