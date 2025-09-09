using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Address
{
    [Key]
    public Guid Id { get; set; }

    [Column(TypeName = "varchar(12)")]
    public required string CEP { get; set; }

    [Column(TypeName = "varchar(64)")]
    public required string Road { get; set; }

    public required int Number { get; set; }

    [Column(TypeName = "varchar(64)")]
    public required string Neighborhood { get; set; }

    [Column(TypeName = "varchar(64)")]
    public required string City { get; set; }

    [Column(TypeName = "varchar(20)")]
    public required string State { get; set; }
}
