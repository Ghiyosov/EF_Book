using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Book
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50), Required]
    public string Title { get; set; }
    public DateTime PublicationDate { get; set; }
    public string Genre { get; set; }
    public int Pages { get; set; }
    public string Language { get; set; }
    public int AuthorId { get; set; }
    public int PublisherId { get; set; }
    
    [ForeignKey("AuthorId")]
    public Author Author { get; set; }
    [ForeignKey("PublisherId")]
    public Publisher Publisher { get; set; }
}