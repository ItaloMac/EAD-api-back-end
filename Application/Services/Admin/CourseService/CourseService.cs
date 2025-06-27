using Application.DTOs.Admin.Course;
using Application.Interfaces;
using Application.Interfaces.Admin;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webAPI.Domain.Models;

public class CourseService : ICourseServices
{
    private readonly UserManager<User> _userManager;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CourseService(IApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<CreateCourseDTO> CreateCourseAsync(CreateCourseDTO dto)
    {
        try
        {
            var newCourse = new Curso
            {
                Status = dto.Status,
                Name = dto.Name,
                Type = dto.Type,
                Presentation = dto.Presentation,
                StartForecast = dto.StartForecast,
                Modality = dto.Modality,
                Location = dto.Modality,
                Workload = dto.Workload,
                Duration = dto.Duration,
                Proposal = dto.Proposal,
                Requirements = dto.Requirements,
                Documentation = dto.Documentation,
                Faculty = dto.Faculty,
                Curriculum = dto.Curriculum,
                RegistrationPrice = dto.RegistrationPrice,
                MonthlyPrice = dto.MonthlyPrice,
                TotalPrice = dto.TotalPrice,
                Installments = dto.Installments,
                CashPrice = dto.CashPrice,
                FullPrice = dto.FullPrice,
                Discount = dto.Discount,
                ImagemUrl = dto.ImagemUrl,
                CoordenadorId = dto.Coordenador.Id
            };
            _context.Cursos.Add(newCourse);
            await _context.SaveChangesAsync();
            return _mapper.Map<CreateCourseDTO>(newCourse);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao criar novo curso", ex);

        }
    }

    public async Task<List<CoursesReponseDTO>> GetAllCoursesAsync()
    {
        try
        {
            var allCourses = await _context.Cursos
            .Include(c => c.Coordenador)
            .ProjectTo<CoursesReponseDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

            return allCourses;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao listar todos os cursos", ex);
        }
    }

    public async Task<CoursesReponseDTO> GetCourseByIdAsync(Guid id)
    {
        try
        {
            var course = await _context.Cursos.
            Include(c => c.Coordenador)
            .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                throw new ApplicationException("Curso não encontrado.");

            return _mapper.Map<CoursesReponseDTO>(course);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao listar o curso", ex);
        }
    }
}
