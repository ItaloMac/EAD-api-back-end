using Application.Interfaces;
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

}