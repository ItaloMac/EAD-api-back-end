namespace Application.DTOs.Admin.Aula;

public class AulaResponseDTO
{
    public Guid Id { get; set; }
    public string Theme { get; set; } = string.Empty;

    public string StartDate { get; set; } = string.Empty;

    public string Classroom { get; set; } = string.Empty;
    public Guid ModuloId { get; set; }
    public string VideoUrl { get; set; } = string.Empty;
}
