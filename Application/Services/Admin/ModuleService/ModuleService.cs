using Application.DTOs.Admin.Module;
using Application.Interfaces;
using Application.Interfaces.Admin;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Admin.ModuleService;

public class ModuleService : IModuleService
{
    private readonly UserManager<User> _userManager;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ModuleService(IApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public Task<List<ModuleResponseDTO>> GetAllModulesAsync()
    {
        try
        {
            var modules = _context.Modulos.ToList();
            var moduleDtos = _mapper.Map<List<ModuleResponseDTO>>(modules);
            return Task.FromResult(moduleDtos);
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu ao listar todos os modulos", ex);
        }
   }
}
