using System;
using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces;

public interface IProfessorService
{
    public Task<List<ProfessorDTO>> GetAllProfessores();    
    public Task<List<ProfessorDTO>> GetByIdProfessores();
    public Task<List<ProfessorDTO>> GetProfessoresByIdCursoAsync(Guid cursoId);
}
