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
}
