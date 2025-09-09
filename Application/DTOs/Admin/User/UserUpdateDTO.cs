using Domain.Models;

namespace Application.DTOs.Admin.User;

public class UserUpdateDTO
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; } = null!;
    public string? CPF { get; set; }
    public string? BirthDate { get; set; }
    public string? ProfilePhoto { get; set; }
    public UserType? UserType { get; set; }
    public string? CustomerId { get; set; }
}
