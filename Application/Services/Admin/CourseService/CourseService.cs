using Application.DTOs.Admin.Course;
using Application.Interfaces;
using Application.Interfaces.Admin;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
}
