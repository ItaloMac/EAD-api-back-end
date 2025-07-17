using System;
using Application.DTOs.Admin.Class;
using Application.Interfaces;
using Application.Interfaces.Admin;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Admin.ClassService;

public class ClassService : IClassServices
{
    private readonly UserManager<User> _userManager; 
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ClassService(IApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<CreateClassDTO> CreateClassAsync(CreateClassDTO dto)
    {
        try
        {
            var registrationIds = dto.RelatedRegistrations.Select(r => r.Id).ToList();
            var registrations = await _context.Registrations
                .Where(r => registrationIds.Contains(r.Id))
                .ToListAsync();

            var newClass = new Class
            {
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CursoId = dto.RelatedCourse.Id,
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
}
