namespace Application.DTOs.Admin.Aula;

public class AulaResponseDTO
{
    public string Theme { get; set; } = null!;

    public string StartDate { get; set; } = null!;

    public string Classroom { get; set; } = null!;
    public ModuloDTO Modulo { get; set; } = null!;
}

public class ModuloDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}