namespace Application.DTOs;
public class AulaDTO
{
    public required string Theme { get; set; } = null!;
    public required string StartDate { get; set; } = null!;
    public required string Classroom { get; set; } = null!;

    // Relacionamento 1:N - Uma Aula pertence a um Módulo
    public Guid Id_Modulo { get; set; }

}
