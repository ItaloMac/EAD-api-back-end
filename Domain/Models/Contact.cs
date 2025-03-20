using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;
public class Contact
{
    [Key]
    public Guid Id { get; set; }

    [Column(TypeName = "Varchar(100)")]
    public required string Name { get; set;}

    [Column(TypeName = "Varchar(254)")]
    public required string Email { get; set;}

    [Column(TypeName = "Varchar(20)")]
    public required string Phone { get; set;}
}
