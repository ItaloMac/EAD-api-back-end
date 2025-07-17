using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.DTOs.Admin.Class;

public class CreateClassDTO
{
    [Column(TypeName = "varchar(64)")]
    public required string Name { get; set; }

    [DataType(DataType.Date)]
    public required DateTime StartDate { get; set; }

    [DataType(DataType.Date)]
    public required DateTime EndDate { get; set; }

    public RelatedCourseDTO RelatedCourse { get; set; } = null!;

    public List<RelatedRegistrationDTO> RelatedRegistrations { get; set; } = new();
}

public class RelatedCourseDTO
{
    public Guid Id { get; set; }
}

public class RelatedRegistrationDTO
{
    public Guid Id { get; set; }
}