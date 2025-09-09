namespace Application.DTOs.Admin.Registration;

public class RegistrationResponseDTO
{
    public Guid Id { get; set; }
    public string RegistrationStatus { get; set; } = null!;
    public DateTime RegistrationDate { get; set; }
    public DateTime? CancellationDate { get; set; }
    public required ClassDTO Class { get; set; }
    public required StudentDTO User { get; set; }
    public string? VindiPlanId { get; set; }
}

public class StudentDTO
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? CPF { get; set; }
    public string? PhoneNumber { get; set; }
    public string ProfilePhoto { get; set; } = null!;

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