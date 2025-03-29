using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webAPI.Domain.Models;

namespace Domain.Models;

public class Class
{
    [Key]
    public Guid Id { get; set; }

    [Column(TypeName = "varchar(64)")]
    public required string Name { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public required DateTime StartDate { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public required DateTime EndDate { get; set; }

    [ForeignKey("Curso")]
    public required Guid? CursoId { get; set; }
    public virtual Curso? Curso { get; set; } = null!;
    public List<Registration> Registrations { get; set; } = new List<Registration>();

}
