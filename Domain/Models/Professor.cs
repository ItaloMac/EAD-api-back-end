using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using webAPI.Domain.Models;

namespace Domain.Models;

public class Professor
{
    [Key]
    public Guid Id { get; set; }
    [Column(TypeName = "varchar(64)")]
    public required string Name { get; set; } = null!;
    [Column(TypeName = "varchar(500)")]
    public required string MiniResume { get; set; } = null!;

    public string ImagemUrl { get; set; } = null!;
    public List<CursoProfessor> CursoProfessores { get; set; } = new();
    public List<Modulo> Modulos { get; set; } = null!;
    public List<Curso> CursosCoordenados { get; set; } = new List<Curso>();
}