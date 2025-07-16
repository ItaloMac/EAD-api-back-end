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
}
