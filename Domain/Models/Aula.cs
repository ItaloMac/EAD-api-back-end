using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Aula
{
    [Key]
    public Guid Id { get; set; }

    [Column(TypeName = "varchar(200)")]
    public required string Theme { get; set; } = null!;

    [Column(TypeName = "varchar(200)")]
    public required string StartDate { get; set; } = null!;

    [Column(TypeName = "varchar(200)")]
    public required string Classroom { get; set; } = null!;

    // Relacionamento 1:N - Uma Aula pertence a um Módulo
    public Guid ModuloId { get; set; }

    public Modulo Modulo { get; set; } = null!;
}
