using System.Reflection;
using Application.DTOs.Admin.Module;
using Application.Interfaces;
using Application.Interfaces.Admin;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

    public async Task<CreateModuleDTO> CreateModuleAsync(CreateModuleDTO dto)
    {
        try
        {
            var newModule = _mapper.Map<Domain.Models.Modulo>(dto);

            _context.Modulos.Add(newModule);
            await _context.SaveChangesAsync();

            return _mapper.Map<CreateModuleDTO>(newModule);
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu ao criar o modulo", ex);
        }
    }

    public async Task<List<ModuleResponseDTO>> GetAllModulesAsync()
    {
        try
        {
           var modulos = await _context.Modulos
        .Include(m => m.Curso)
        .Include(m => m.Professor)
        .Include(m => m.Aulas)
        .ToListAsync();

        return _mapper.Map<List<ModuleResponseDTO>>(modulos);
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu ao listar todos os modulos", ex);
        }
   }

    public Task<ModuleResponseDTO> GetModuleByIdAsync(Guid id)
    {
        try
        {
            var module = _context.Modulos
            .Include(m => m.Curso)
            .Include(m => m.Professor)
            .Include(m => m.Aulas)
            .FirstOrDefault(m => m.Id == id);

            if (module == null)
            {
                throw new Exception("Modulo não encontrado");
            }
            var moduleDto = _mapper.Map<ModuleResponseDTO>(module);
            return Task.FromResult(moduleDto);
        }
        catch (Exception ex)
        {
            throw new Exception($"Ocorreu ao buscar o modulo com ID {id}", ex);
        }
    }

    public async Task<CreateModuleDTO> UpdateModuleAsync(Guid id, CreateModuleDTO dto)
    {
        try
        {
            var existingModule = await _context.Modulos.FindAsync(id);
            if (existingModule == null)
            {
                throw new Exception("Modulo não encontrado");
            }

            _mapper.Map(dto, existingModule);
            _context.Modulos.Update(existingModule);
            await _context.SaveChangesAsync();

            return _mapper.Map<CreateModuleDTO>(existingModule);
        }
        catch (Exception ex)
        {
            throw new Exception($"Ocorreu ao atualizar o modulo com ID {id}", ex);
        }
    }
}
