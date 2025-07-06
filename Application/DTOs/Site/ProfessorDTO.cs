namespace Application.DTOs;
public class ProfessorDTO
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string MiniResume { get; set; }
}
