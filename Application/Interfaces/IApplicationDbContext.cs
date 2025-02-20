using Microsoft.EntityFrameworkCore;

using webAPI.Domain.Models;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Curso> Cursos { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
