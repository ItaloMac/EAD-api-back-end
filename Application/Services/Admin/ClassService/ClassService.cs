using System.Globalization;
using Application.DTOs.Admin.Class;
using Application.Interfaces;
using Application.Interfaces.Admin;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Admin.ClassService;

public class ClassService : IClassServices
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ClassService(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CreateClassDTO> CreateClassAsync(CreateClassDTO dto)
    {
        try
        {
            var registrationIds = dto.RelacionedRegistrations.Select(r => r.Id).ToList();
            var registrations = await _context.Registrations
                .Where(r => registrationIds.Contains(r.Id))
                .ToListAsync();

            var newClass = new Class
            {
                Name = dto.Name,
                StartDate = DateTime.ParseExact(dto.StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact(dto.EndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                CursoId = dto.RelacionedCourse.Id,
                Registrations = registrations
            };

            _context.Classes.Add(newClass);
            await _context.SaveChangesAsync();
            return _mapper.Map<CreateClassDTO>(newClass);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao criar a turma", ex);
        }
    }

    public async Task<bool> DeleteClassAsync(Guid id)
    {
        try{
            var classToDelete = await _context.Classes.FirstOrDefaultAsync(c => c.Id == id);
            if (classToDelete == null)
            {
                throw new ApplicationException("Turma não encontrada.");
            }
            _context.Classes.Remove(classToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao deletar a turma", ex);
        }
    }

    public async Task<List<ClassResponseDTO>> GetAllClassesAsync()
    {
        try
        {
            var allClass = await _context.Classes
            .Include(c => c.Curso)
            .Include(r => r.Registrations)
            .ProjectTo<ClassResponseDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

            return allClass;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao listar todas as turmas", ex);
        }
    }

    public async Task<ClassResponseDTO> GetClassById(Guid id)
    {
        try
        {
            var Class = await _context.Classes
            .Include(c => c.Curso)
            .Include(r => r.Registrations)
            .ProjectTo<ClassResponseDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cl => cl.Id == id);

            if (Class == null)
                throw new ApplicationException("Turma não encontrada.");

            return Class;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao listar a turma", ex);
        }
    }

    public async Task<CreateClassDTO> UpdateClassAsync(Guid id, CreateClassDTO dto)
    {
        try
        {
            var classExisting = await _context.Classes.FirstOrDefaultAsync(r => r.Id == id);

            if (classExisting == null)
            {
                throw new ApplicationException("Turma não encontrada.");
            }

            var registrationIds = dto.RelacionedRegistrations.Select(r => r.Id).ToList();
            var registrations = await _context.Registrations
                .Where(r => registrationIds.Contains(r.Id))
                .ToListAsync();

            classExisting.Name = dto.Name;
            classExisting.StartDate = DateTime.ParseExact(dto.StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            classExisting.EndDate = DateTime.ParseExact(dto.EndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            classExisting.CursoId= dto.RelacionedCourse.Id;
            classExisting.Registrations = registrations;
            _context.Classes.Update(classExisting);
            await _context.SaveChangesAsync();
            return _mapper.Map<CreateClassDTO>(classExisting);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao atualizar a turma", ex);
        }
    }
}
