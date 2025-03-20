using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using webAPI.Domain.Models;

namespace Infrastucture;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Aula> Aulas { get; set; }
    public DbSet<Modulo> Modulos { get; set; }
    public DbSet<Professor> Professores { get; set; }
    public DbSet<CursoProfessor> CursoProfessores { get; set; }
    public DbSet<Contact> Contacts { get; set; }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuração do relacionamento N:N entre Curso e Professor (CursoProfessor)
        modelBuilder.Entity<CursoProfessor>()
            .HasKey(cp => new { cp.Id_Curso, cp.Id_Professor }); // Define a chave composta

        modelBuilder.Entity<CursoProfessor>()
            .HasOne(cp => cp.Curso)
            .WithMany(c => c.CursoProfessores) // ⬅️ Ajustado para refletir a tabela intermediária
            .HasForeignKey(cp => cp.Id_Curso);

        modelBuilder.Entity<CursoProfessor>()
            .HasOne(cp => cp.Professor)
            .WithMany(p => p.CursoProfessores) // ⬅️ Ajustado para refletir a tabela intermediária
            .HasForeignKey(cp => cp.Id_Professor);

        modelBuilder.Entity<Curso>()
            .HasOne(c => c.Coordenador)  // Curso tem um coordenador
            .WithMany(p => p.CursosCoordenados)  // Professor pode coordenar vários cursos
            .HasForeignKey(c => c.CoordenadorId)  // A chave estrangeira
            .OnDelete(DeleteBehavior.Cascade);
    }
}
