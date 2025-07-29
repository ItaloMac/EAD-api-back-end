namespace Application.DTOs.Admin.Module;

public class ModuleResponseDTO
{
    public Guid Id { get; set; }
    public string Theme { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string StartDate { get; set; } = null!;
    public string EndDate { get; set; } = null!;
    public string Workload { get; set; } = null!;
    public Curso Curso { get; set; } = null!; 
    public Professor Professor { get; set; } = null!;
    public List<Aula> Aulas { get; set; } = new List<Aula>();
}

public class Curso
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}

public class Professor
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}

public class Aula
{
    public Guid Id { get; set; }
    public string Theme { get; set; } = null!;
}