using webAPI.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class CursoProfessor
{
    public Guid Id_Curso { get; set; }
    public Curso Curso { get; set; } = null!;

    public Guid Id_Professor { get; set; }
    public Professor Professor { get; set; } = null!;
}
