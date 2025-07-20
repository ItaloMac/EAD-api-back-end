using System;
using Application.DTOs;
using Application.DTOs.Admin.Aula;
using Application.Interfaces;
using Application.Interfaces.Admin;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Admin.AulaService;

public class AulaService : IAulasService
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

    public Task<AulaResponseDTO> GetAulaByIdAsync(Guid id)
    {
        try
        {
            var aula = _context.Aulas.Find(id);
            if (aula == null)
            {
                throw new Exception("Aula não encontrada.");
            }
            var aulaResponseDTO = _mapper.Map<AulaResponseDTO>(aula);
            return Task.FromResult(aulaResponseDTO);
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao buscar a aula.", ex);
        }
    }

    Task<List<AulaResponseDTO>> IAulasService.GetAllAulasAsync()
    {
        try
        {
            var aulas = _context.Aulas.ToList();
            var aulaResponseDTOs = _mapper.Map<List<AulaResponseDTO>>(aulas);
            return Task.FromResult(aulaResponseDTOs);
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao listar todas as aulas.", ex);
        }
    }
}
