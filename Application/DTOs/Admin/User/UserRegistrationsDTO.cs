namespace Application.DTOs.Admin.User;

public class UserRegistrationDTO
{
    public Guid Id { get; set; }
    public string RegistrationStatus { get; set; } = null!;
    public DateTime RegistrationDate { get; set; }
    public DateTime? CancellationDate { get; set; }
    public required ClassDTO Class { get; set; }
}

public class ClassDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public required CourseDTO Curso { get; set; } 

}

public class CourseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}