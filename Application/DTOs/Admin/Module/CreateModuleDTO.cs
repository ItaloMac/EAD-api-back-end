namespace Application.DTOs.Admin.Module;

public class CreateModuleDTO
{
    public string Theme { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string StartDate { get; set; } = null!;
    public string EndDate { get; set; } = null!;
    public string Workload { get; set; } = null!;
    public CursoDTO Curso { get; set; } = null!; 
    public ProfessorDTO Professor { get; set; } = null!;
    public List<AulaDTO> Aulas { get; set; } = new List<AulaDTO>();
}

public class CursoDTO
{
    public Guid Id { get; set; }
}

public class ProfessorDTO
{
    public Guid Id { get; set; }
}

public class AulaDTO
{
    public Guid Id { get; set; }
}