using System;
using Microsoft.AspNetCore.Identity;

namespace Application.DTOs.Admin.Registration;

public class RegistrationResponseDTO
{
    public string RegistrationStatus { get; set; } = null!;
    public DateTime RegistrationDate { get; set; }
    public DateTime? CancellationDate { get; set; }
    public required ClassDTO Class { get; set; }
    public required StudentDTO User { get; set; }
}

public class StudentDTO
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? CPF { get; set; }
    public string? PhoneNumber { get; set; }
}

public class ClassDTO
{
    public string Name { get; set; } = null!;
    public required CourseDTO Curso { get; set; }

}

public class CourseDTO
{
    public string Name { get; set; } = null!;
}