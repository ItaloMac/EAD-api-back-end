using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.DTOs.Admin.Class;

public class CreateClassDTO
{
    [Column(TypeName = "varchar(64)")]
    public required string Name { get; set; }
    public required string StartDate { get; set; }
    public required string EndDate { get; set; }

    public RelatedCourseDTO RelacionedCourse { get; set; } = null!;

    public List<RelatedRegistrationDTO> RelacionedRegistrations { get; set; } = new();
}

public class RelatedCourseDTO
{
    public Guid Id { get; set; }
}

public class RelatedRegistrationDTO
{
    public Guid Id { get; set; }
}