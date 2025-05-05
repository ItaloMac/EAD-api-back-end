using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class RegisterUserRequest
{
    [EmailAddress(ErrorMessage = "Email inválido")]
    public required string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; } 
    public required string CPF { get; set; }
    public required string Name { get; set; }
    public required string LastName { get; set; }
}