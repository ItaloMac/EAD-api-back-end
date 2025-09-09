using System;

namespace Application.DTOs.Admin.Course;

public class CourseTeacherResponseDTO
{
    public required Guid Id { get; set; }
    public required string Name { get; set; } = null!;
    public required string MiniResume { get; set; } = null!;
    public string ImagemUrl { get; set; } = null!;
}