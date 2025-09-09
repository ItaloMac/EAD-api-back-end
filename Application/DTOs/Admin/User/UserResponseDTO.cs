using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Application.DTOs.Admin.User;

public class UserResponseDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }

    [EmailAddress(ErrorMessage = "O e-mail fornecido não é válido.")]
    public string Email { get; set; } = null!;
    public string? CPF { get; set; }
    public string? BirthDate { get; set; }
    public string? ProfilePhoto { get; set; }
    public string? PhoneNumber { get; set; }
    public UserType? UserType { get; set; }
    public string? CustomerId { get; set; }
    public Guid? AddressId { get; set; }
}