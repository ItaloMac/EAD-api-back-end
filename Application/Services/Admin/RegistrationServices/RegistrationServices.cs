using System.Globalization;
using Application.DTOs.Admin.Registration;
using Application.Interfaces;
using Application.Interfaces.Admin;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Admin.RegistrationService;

public class RegistrationServices : IRegistrationService
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public RegistrationServices(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<RegistrationResponseDTO>> GetAllRegistrations()
    {
        try
        {
            var registrations = await _context.Registrations
                .Include(r => r.Class)
                    .ThenInclude(c => c.Curso)
                .Include(r => r.User)
                .ProjectTo<RegistrationResponseDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return registrations;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao listar as matriculas", ex);
        }
    }

    public async Task<RegistrationResponseDTO> GetRegistrationById(Guid id)
    {
        try
        {
            var registration = await _context.Registrations
            .Include(r => r.User)
            .Include(r => r.Class)
                .ThenInclude(c => c.Curso)
            .FirstOrDefaultAsync(r => r.Id == id);

            if (registration == null)
                throw new ApplicationException("Matrícula não encontrada.");

            return _mapper.Map<RegistrationResponseDTO>(registration);

        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao listar a matricula", ex);
        }
    }

    public async Task<CreateRegistrationDTO> PostRegistrationAsync(CreateRegistrationDTO dto)
    {
        try
        {
            var NewRegistration = new Registration
            {
                RegistrationStatus = dto.RegistrationStatus,
                RegistrationDate = DateTime.ParseExact(dto.RegistrationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                CancellationDate = string.IsNullOrWhiteSpace(dto.CancellationDate) 
                    ? null 
                    : DateTime.ParseExact(dto.CancellationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                ClassId = dto.Class.Id,
                UserId = dto.User.Id,
                VindiPlanId = dto.VindiPlanId
            };

            _context.Registrations.Add(NewRegistration);
            await _context.SaveChangesAsync();
            return _mapper.Map<CreateRegistrationDTO>(NewRegistration);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao criar matrícula", ex);
        }
    }

    public async Task<UpdateRegistrationDTO> PutRegistrationAsync(Guid Id, UpdateRegistrationDTO registrationDTO)
    {
        try
        {
            var registrationExisting = await _context.Registrations.FirstOrDefaultAsync(r => r.Id == Id);

            if (registrationExisting == null)
            {
                throw new ApplicationException("Matrícula não encontrada.");
            }

            registrationExisting.RegistrationStatus = registrationDTO.RegistrationStatus;
            registrationExisting.RegistrationDate = DateTime.ParseExact(registrationDTO.RegistrationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            registrationExisting.CancellationDate = string.IsNullOrWhiteSpace(registrationDTO.CancellationDate)
                ? null
                : DateTime.ParseExact(registrationDTO.CancellationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            registrationExisting.ClassId = registrationDTO.Class.Id;
            registrationExisting.UserId = registrationDTO.User.Id;

            _context.Registrations.Update(registrationExisting);
            await _context.SaveChangesAsync();
            return _mapper.Map<UpdateRegistrationDTO>(registrationExisting);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao atualizar a matrícula", ex);
        }
    }

    public async Task<bool> DeleteRegistrationAsync(Guid Id)
    {
        try
        {
            var registrationToDelete = await _context.Registrations.FirstOrDefaultAsync(r => r.Id == Id);
            if (registrationToDelete == null)
            {
                throw new ApplicationException("Matrícula não encontrada.");
            }
            _context.Registrations.Remove(registrationToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao excluir matrícula", ex);
        }
    }
}
