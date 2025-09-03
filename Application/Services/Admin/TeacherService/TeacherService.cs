using Application.DTOs.Admin.Teacher;
using Application.Interfaces;
using Application.Interfaces.Admin;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Admin.TeacherService;

public class TeacherService : ITeacherServices
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public TeacherService(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CreateTeacherDTO> CreateTeacherAsync(CreateTeacherDTO dto)
    {
        try
        {
            var newTeacher = new Professor
            {
                Name = dto.Name,
                MiniResume = dto.MiniResume,
                ImagemUrl = dto.ImagemUrl
            };

            _context.Professores.Add(newTeacher);
            await _context.SaveChangesAsync();
            return _mapper.Map<CreateTeacherDTO>(newTeacher);

        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao criar o professor", ex);
        }
    }

    public async Task<bool> DeleteTeacherAsync(Guid id)
    {
       try
        {
            var teacherToDelete = await _context.Professores.FirstOrDefaultAsync(r => r.Id == id);
            if (teacherToDelete == null)
            {
                throw new ApplicationException("Professor não encontrado.");
            }
            _context.Professores.Remove(teacherToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao excluir professor", ex);
        }
    }

    public async Task<List<TeacherResponseDTO>> GetAllTeachers()
    {
        try
        {
            var allTeachers = await _context.Professores
                .ProjectTo<TeacherResponseDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return allTeachers;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao listar todos os professores", ex);
        }
    }

    public async Task<List<TeacherCourseResponseDTO>> GetCoursesByIdTeacherAsync(Guid id)
    {
        try
        {
            var teacher = await _context.Professores.FirstOrDefaultAsync(c => c.Id == id);
            if (teacher == null)
            {
                throw new ApplicationException("Professor não encontrado.");
            }

            var relatedCourses = await _context.CursoProfessores.
            Where(cp => cp.Id_Professor == id)
            .Select(cp => new TeacherCourseResponseDTO
            {
                Name = cp.Curso.Name,
                Status = cp.Curso.Status,
                Modality = cp.Curso.Modality,
                Type = cp.Curso.Type
                
            })
            .ToListAsync();
            return relatedCourses;

        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao listar os cursos do professor", ex);
        }
    }

    public async Task<List<TeacherModulesDTO>> GetModulesByIdTeacherAsync(Guid id)
    {
         try
        {
            var teacher = await _context.Professores.FirstOrDefaultAsync(c => c.Id == id);
            if (teacher == null)
            {
                throw new ApplicationException("Professor não encontrado.");
            }

            var relatedModules = await _context.Modulos.
            Where(cp => cp.Id_Professor == id)
            .Select(cp => new TeacherModulesDTO
            {
                Id = cp.Id,
                Theme = cp.Theme,
                StartDate = cp.StartDate,
                EndDate = cp.EndDate
            })
            .ToListAsync();
            return relatedModules;

        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao listar os modulos do professor", ex);
        }
    }

    public async Task<TeacherResponseDTO> GetTeacherByID(Guid id)
    {
        try
        {
            var teacher = await _context.Professores.FirstOrDefaultAsync(p => p.Id == id);
            if (teacher == null)
            throw new ApplicationException("Professor não encontrado.");

            return _mapper.Map<TeacherResponseDTO>(teacher);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao listar o professor", ex);
        }
    }

    public async Task<UpdateTeacherDTO> UpdateTeacherAsync(Guid id, UpdateTeacherDTO dto)
    {
        try
        {
            var teacherExisting = await _context.Professores.FirstOrDefaultAsync(r => r.Id == id);

            if (teacherExisting == null)
            {
                throw new ApplicationException("Professor não encontrada.");
            }

            teacherExisting.Name = dto.Name;
            teacherExisting.MiniResume = dto.MiniResume;
            teacherExisting.ImagemUrl = dto.ImagemUrl;

            _context.Professores.Update(teacherExisting);
            await _context.SaveChangesAsync();
            return _mapper.Map<UpdateTeacherDTO>(teacherExisting);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao atualizar o professor", ex);
        }
    }
}
