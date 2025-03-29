using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Registration
{
    [Key]
    public Guid Id { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public required DateTime RegistrationDate { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? CancellationDate { get; set; }

    [ForeignKey("User")]
    public required Guid UserId { get; set; }
    public virtual User User { get; set; }  = null!;

    [ForeignKey("Class")]
    public required Guid ClassId { get; set; }
    public virtual Class Class { get; set; }  = null!;

    [Column(TypeName = "varchar(10)")]
    public string RegistrationStatus { get; set; } = "ativo"; 

    [Column(TypeName = "varchar(64)")]
    public string? VindiPlanId { get; set; }
}
