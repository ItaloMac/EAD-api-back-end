using Domain.Models;
using Microsoft.EntityFrameworkCore;
using webAPI.Domain.Models;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Curso> Cursos { get; set; }
    DbSet<Professor> Professores { get; set; }
    DbSet<Aula> Aulas { get; set; }
    DbSet<Modulo> Modulos { get; set; }
    DbSet<CursoProfessor> CursoProfessores { get ; set; }
    DbSet<Contact> Contacts { get; set; }
    DbSet<Registration> Registrations { get; set; }
    DbSet<Class> Classes { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
