using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using webAPI.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Infrastucture;public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IApplicationDbContext
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
    public DbSet<Address> Addresses { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Registration> Registrations { get; set; }
    public DbSet<Class> Classes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configuração do relacionamento N:N entre Curso e Professor (CursoProfessor)
        modelBuilder.Entity<CursoProfessor>()
            .HasKey(cp => new { cp.Id_Curso, cp.Id_Professor });
        modelBuilder.Entity<CursoProfessor>()
            .HasOne(cp => cp.Curso)
            .WithMany(c => c.CursoProfessores)
            .HasForeignKey(cp => cp.Id_Curso);

        modelBuilder.Entity<CursoProfessor>()
            .HasOne(cp => cp.Professor)
            .WithMany(p => p.CursoProfessores)
            .HasForeignKey(cp => cp.Id_Professor);

        modelBuilder.Entity<Curso>()
            .HasOne(c => c.Coordenador)
            .WithMany(p => p.CursosCoordenados)
            .HasForeignKey(c => c.CoordenadorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Aula>()
            .HasOne(a => a.Modulo)
            .WithMany(m => m.Aulas)
            .HasForeignKey(a => a.ModuloId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .Property(u => u.BirthDate)
            .HasColumnType(null);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.CPF)
            .IsUnique();
        
       modelBuilder.Entity<User>()
            .Property(u => u.UserType)
            .HasDefaultValue(UserType.Aluno) // Valor padrão explícito
            .HasConversion<int>() // Converte para int no banco
            .HasSentinel((UserType)(-1)); // Valor sentinela que nunca será usado
    }
}
