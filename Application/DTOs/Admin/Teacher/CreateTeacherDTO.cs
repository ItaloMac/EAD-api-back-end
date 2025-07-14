using System;

namespace Application.DTOs.Admin.Teacher;

public class CreateTeacherDTO
{
    public required string Name { get; set; } = null!;
    public required string MiniResume { get; set; } = null!;
    public string ImagemUrl { get; set; } = null!;
}
