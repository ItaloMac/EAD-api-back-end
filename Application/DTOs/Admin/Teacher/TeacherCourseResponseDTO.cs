using System;

namespace Application.DTOs.Admin.Teacher;

public class TeacherCourseResponseDTO
{
    public required string Name { get; set; } = null!;
    public required bool Status { get; set; }
    public required string Modality { get; set; } = null!;
    public required string Type { get; set; } = null!;
}
