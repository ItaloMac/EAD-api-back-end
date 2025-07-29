using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.DTOs.Admin.Class;

public class ClassResponseDTO
{
    public Guid Id { get; set; }

    [Column(TypeName = "varchar(64)")]
    public required string Name { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public required DateTime StartDate { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public required DateTime EndDate { get; set; }
    public RelacionedCourse relacionedCourse { get; set; } = null!;
    public List<RelacionedRegistration> relacionedRegistrations { get; set; } = new();
}

public class RelacionedCourse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}

public class RelacionedRegistration
{
    public Guid Id { get; set; }
}