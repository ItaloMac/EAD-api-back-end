using System;
using Application.DTOs.Admin.Registration;
using Application.Interfaces;
using Application.Interfaces.Admin;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Admin.RegistrationService;

public class RegistrationServices : IRegistrationService
{
    private readonly UserManager<User> _userManager;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public RegistrationServices(IApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
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
        catch(Exception ex)
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
            throw new KeyNotFoundException("Matrícula não encontrada.");

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
                RegistrationDate = dto.RegistrationDate,
                CancellationDate = dto.CancellationDate,
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

    public Task<UpdateRegistrationDTO> PutRegistrationAsync(Guid Id, UpdateRegistrationDTO registrationDTO)
    {
        throw new NotImplementedException();
    }
}
