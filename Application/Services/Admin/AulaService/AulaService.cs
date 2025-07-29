using Application.DTOs.Admin.Aula;
using Application.Interfaces;
using Application.Interfaces.Admin;
using AutoMapper;
using Domain.Models;

namespace Application.Services.Admin.AulaService;

public class AulaService : IAulasService
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AulaService(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CreateAulaDTO> CreateAulaAsync(CreateAulaDTO createAulaDTO)
    {
        try
        {
            var aula = _mapper.Map<Aula>(createAulaDTO);
            _context.Aulas.Add(aula);
            await _context.SaveChangesAsync();

            return _mapper.Map<CreateAulaDTO>(aula);
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao criar a aula.", ex);
        }
    }

    public async Task<bool> DeleteAulaAsync(Guid id)
    {
        try
        {
            var aula = _context.Aulas.Find(id);
            if (aula == null)
            {
                throw new Exception("Aula não encontrada.");
            }

            _context.Aulas.Remove(aula);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao deletar a aula.", ex);
        }
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

    public async Task<CreateAulaDTO> UpdateAulaAsync(Guid id, CreateAulaDTO updateAulaDTO)
    {
        try
        {
            var aula = _context.Aulas.Find(id);
            if (aula == null)
            {
                throw new Exception("Aula não encontrada.");
            }

            _mapper.Map(updateAulaDTO, aula);
            _context.Aulas.Update(aula);
            await _context.SaveChangesAsync();

            return _mapper.Map<CreateAulaDTO>(aula);
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu um erro ao atualizar a aula.", ex);
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
