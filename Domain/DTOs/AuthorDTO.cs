using Domain.Entities;

namespace Domain.DTOs;


public class AuthorDTO
{
    public string Name { get; set; }
    public string Biography { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Nationality { get; set; }
    public string Awards { get; set; }
}

public class CreateAuthorDTO : AuthorDTO;

public class UpdateAuthorDTO : CreateAuthorDTO
{
    public int Id { get; set; }
}

public class ReadAutorDTO : UpdateAuthorDTO
{
    public List<UpdateBookDTO> Books { get; set; }
}
