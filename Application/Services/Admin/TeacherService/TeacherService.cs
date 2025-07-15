using System;
using Application.DTOs.Admin.Teacher;
using Application.Interfaces;
using Application.Interfaces.Admin;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Admin.TeacherService;

public class TeacherService : ITeacherServices
{
    private readonly UserManager<User> _userManager;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public TeacherService(IApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
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
