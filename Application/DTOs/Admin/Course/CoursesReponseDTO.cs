namespace Application.DTOs.Admin.Course;

public class CoursesReponseDTO
{
    public Guid Id { get; set; }
    public bool Status { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public string? Presentation { get; set; }
    public required string StartForecast { get; set; }
    public required string Modality { get; set; }
    public required string Location { get; set; }
    public required string Workload { get; set; }
    public required string Duration { get; set; }
    public required string Proposal { get; set; }
    public required string Requirements { get; set; }
    public required string Documentation { get; set; }
    public required string Faculty { get; set; }
    public required string Curriculum { get; set; }
    public required string RegistrationPrice { get; set; }
    public required string MonthlyPrice { get; set; }
    public required string TotalPrice { get; set; }
    public required string Installments { get; set; }
    public required string CashPrice { get; set; }
    public required string FullPrice { get; set; }
    public string Discount { get; set; } = null!;
    public string ImagemUrl { get; set; } = null!;
    public CoordenadorCourse? Coordenador { get; set; }
}

public class CoordenadorCourse
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
}
