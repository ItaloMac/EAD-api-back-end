using System;

namespace Application.DTOs.Admin.Aula;

public class UpdateAulaDTO
{
    public string Theme { get; set; } = null!;
    public string StartDate { get; set; } = null!;
    public string Classroom { get; set; } = null!;
    public Guid ModuloId { get; set; }
    public string VideoUrl { get; set; } = string.Empty;
}
