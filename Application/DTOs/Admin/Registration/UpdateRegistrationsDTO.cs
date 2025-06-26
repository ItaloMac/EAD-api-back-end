namespace Application.DTOs.Admin.Registration;

public class UpdateRegistrationDTO
{
    public string RegistrationStatus { get; set; } = null!;
    public DateTime RegistrationDate { get; set; }
    public DateTime? CancellationDate { get; set; }
    public required ClassIdDTO Class { get; set; }
    public required UserIdDTO User { get; set; }
    public string? VindiPlanId { get; set; }
}
