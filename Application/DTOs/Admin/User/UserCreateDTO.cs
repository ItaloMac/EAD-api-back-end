using System;
using Domain.Models;

namespace Application.DTOs.Admin.User;

public class UserCreateDTO
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? CPF { get; set; }
    public string? BirthDate { get; set; }
    public string? ProfilePhoto { get; set; }
    public UserType UserType { get; set; } = UserType.Aluno;
    public string? CustomerId { get; set; }
}
