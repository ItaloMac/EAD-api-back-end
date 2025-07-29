public class ModuloDTO
{
    public Guid Id { get; set; }
    public required string Theme { get; set; }
    public required string Description { get; set; }
    public required string StartDate { get; set; }
    public required string EndDate { get; set; }
    public required string WorkLoad { get; set; }
    
    // Relacionamento 1:N - Um Curso tem vários Módulos
    public required Guid Id_Curso { get; set; }

    // Relacionamento 1:1 - Um Módulo tem um Professor
    public required Guid Id_Professor { get; set; }

    // Relacionamento 1:N - Um Módulo tem várias Aulas
    public required List<Guid> Ids_Aulas { get; set; }
}