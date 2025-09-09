namespace Application.DTOs.Admin.Registration;

public class CreateRegistrationDTO
{
    public string RegistrationStatus { get; set; } = null!;
    public string RegistrationDate { get; set; } = null!;
    public string? CancellationDate { get; set; }
    public required ClassIdDTO Class { get; set; }
    public required UserIdDTO User { get; set; }
    public string? VindiPlanId { get; set; }
}

public class UserIdDTO
{
   public Guid Id { get; set; }
}

public class ClassIdDTO
{
    public Guid Id { get; set; }
}

public class CourseIdDTO
{
    public Guid Id { get; set; }
}