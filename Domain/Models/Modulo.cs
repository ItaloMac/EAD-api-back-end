using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webAPI.Domain.Models;

namespace Domain.Models;

public class Modulo
{
    [Key]
    public Guid Id { get; set; }

    [Column(TypeName = "varchar(255)")]
    public required string Theme { get; set; } = null!;

    [Column(TypeName = "longtext")]
    public required string Description { get; set; }

    [Column(TypeName = "varchar(255)")]
    public required string StartDate { get; set; } = null!;

    [Column(TypeName = "varchar(255)")]
    public required string EndDate { get; set; } = null!;

    [Column(TypeName = "varchar(255)")]
    public required string WorkLoad { get; set; } = null!;

    // Relacionamento 1:N - Um Curso tem vários Módulos
    [ForeignKey("Curso")]
    public Guid Id_Curso { get; set; }
    public Curso Curso { get; set; } = null!;

    // Relacionamento 1:1 - Um Módulo tem um Professor
    [ForeignKey("Professor")]
    public Guid Id_Professor { get; set; }
    public Professor Professor { get; set; } = null!;

    // Relacionamento 1:N - Um Módulo tem várias Aulas
    public List<Aula> Aulas { get; set; } = new List<Aula>();
}
