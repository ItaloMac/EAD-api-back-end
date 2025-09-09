namespace Application.DTOs.Admin.Registration;

public class UpdateRegistrationDTO
{
    public string RegistrationStatus { get; set; } = null!;
    public string RegistrationDate { get; set; } = null!;
    public string? CancellationDate { get; set; }
    public required ClassIdDTO Class { get; set; }
    public required UserIdDTO User { get; set; }
    public string? VindiPlanId { get; set; }
}
