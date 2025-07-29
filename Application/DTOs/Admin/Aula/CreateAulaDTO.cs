namespace Application.DTOs.Admin.Aula;

public class CreateAulaDTO
{
    public string Theme { get; set; } = null!;
    public string StartDate { get; set; } = null!;
    public string Classroom { get; set; } = null!;
    public Guid ModuloId { get; set; }
}
