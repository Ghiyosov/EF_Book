using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Author
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50), Required]
    public string Name { get; set; }
    public string Biography { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Nationality { get; set; }
    public string Awards { get; set; }

    public List<Book> Books { get; set; }
}