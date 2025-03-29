using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class User : IdentityUser<Guid>
{
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string CPF { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? ProfilePhoto { get; set; }
    public string? UserType { get; set; }
    public string? VindiCustomerId { get; set; }

    [ForeignKey("Address")]
    public Guid? AddressId { get; set; }
    public virtual Address? Address { get; set; }
    public List<Registration> Registrations { get; set; } = new List<Registration>();
}