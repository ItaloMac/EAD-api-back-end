namespace Application.DTOs;

public class ContactDTO
{
    public Guid Id { get; set; }
    public required string Name { get; set;}
    public required string Email { get; set;}
    public required string Phone { get; set;}
}
