using System;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Admin.AulaService;

public class AulaService : IAulaService
{
    private readonly UserManager<User> _userManager; 
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AulaService(IApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public Task<List<AulaDTO>> GetAllAulas()
    {
        try
        {
            var aulas = _context.Aulas.ToList();
            var aulaDtos = _mapper.Map<List<AulaDTO>>(aulas);
            return Task.FromResult(aulaDtos);
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao listar todas as aulas.", ex);
        }
    }
}
