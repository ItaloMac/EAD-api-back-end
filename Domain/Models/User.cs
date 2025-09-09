using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? CPF { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? ProfilePhoto { get; set; }
    public string? Password { get; set; }
    public UserType UserType { get; set; } = UserType.Aluno;
    public string? CustomerId { get; set; }
    public bool EmailConfirmed { get; set; } = false;

    [ForeignKey("Address")]
    public Guid? AddressId { get; set; }
    public virtual Address? Address { get; set; }
    public List<Registration> Registrations { get; set; } = new List<Registration>();
}