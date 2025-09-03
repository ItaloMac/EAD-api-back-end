using System;

namespace Application.DTOs.Admin.Teacher;

public class TeacherModulesDTO
{
    public required Guid Id { get; set; }
    public required string Theme { get; set; }
    public required string StartDate { get; set; }
    public required string EndDate { get; set; }
}
