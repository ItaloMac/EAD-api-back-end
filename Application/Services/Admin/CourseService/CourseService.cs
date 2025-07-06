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

    public async Task<AssignCordinatorDTO> AssignCordinatorAsync(Guid CourseId, AssignCordinatorDTO dto)
    {
        try
        {
            var courseExisting = await _context.Cursos.FirstOrDefaultAsync(c => c.Id == CourseId);
            if (courseExisting == null)
                throw new ApplicationException("Curso não encontradO.");

            var professor = await _context.Professores.FirstOrDefaultAsync(p => p.Id == dto.CoordenadorId);
            if (professor == null)
                throw new ApplicationException("Professor não encontrado.");

            courseExisting.CoordenadorId = dto.CoordenadorId;

            _context.Cursos.Update(courseExisting);
            await _context.SaveChangesAsync();
            return dto;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao vincular coordenador ao curso", ex);
        }
    }

    public async Task<CourseTeacherDTO> AssignProfessorToCourseAsync(Guid CourseId, CourseTeacherDTO ProfessorId)
    {
        try
        {
            var course = await _context.Cursos.FirstOrDefaultAsync(c => c.Id == CourseId);
            if (course == null)
            {
                throw new ApplicationException("Curso não encontrado.");
            }

            var newTeacher = new CursoProfessor
            {
                Id_Curso = CourseId,
                Id_Professor = ProfessorId.ProfessorId
            };
            _context.CursoProfessores.Add(newTeacher);
            await _context.SaveChangesAsync();
            return _mapper.Map<CourseTeacherDTO>(newTeacher);

        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao vincular o professor", ex);
        }
    }

    public async Task<List<CourseClassListDTO>> CourseClassListAssync(Guid CourseId)
    {
        try
        {
            var courseClass = await _context.Classes.
            Where(c => c.CursoId == CourseId)
            .ProjectTo<CourseClassListDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

            return courseClass;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao listar as turmas do curso", ex);
        }
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
                Location = dto.Location,
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

    public async Task<bool> DeleteCourseAsync(Guid id)
    {
        try
        {
            var courseToDelete = await _context.Cursos.FirstOrDefaultAsync(c => c.Id == id);
            if (courseToDelete == null)
            {
                throw new ApplicationException("Curso não encontrado.");
            }

            var relatedClass = await _context.Classes.
            Where(classe => classe.CursoId == id)
            .ToListAsync();

           // Excluir todas as matrículas relacionadas a essas turmas
            var relatedClassIds = relatedClass.Select(cl => cl.Id).ToList();
            var relatedRegistrations = await _context.Registrations
                .Where(m => relatedClassIds.Contains(m.ClassId))
                .ToListAsync();
                _context.Registrations.RemoveRange(relatedRegistrations);

          // Excluir as turmas
            _context.Classes.RemoveRange(relatedClass);

            // Excluir o curso
            _context.Cursos.Remove(courseToDelete);

            // Salvar tudo
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao excluir o curso", ex);
        }
    }

    public async Task<bool> DeleteTeacherFromCourseAsync(Guid CourseId, Guid ProfessorId)
    {
        try
        {
            var course = await _context.Cursos.FirstOrDefaultAsync(c => c.Id == CourseId);
            if (course == null)
            {
                throw new ApplicationException("Curso não encontrado.");
            }

            var teacherToDelete = await _context.CursoProfessores
            .FirstOrDefaultAsync(cp => cp.Id_Curso == CourseId && cp.Id_Professor == ProfessorId);
           
            if (teacherToDelete == null)
            {
                throw new ApplicationException("Professor não está vinculado a este curso.");
            }

            _context.CursoProfessores.Remove(teacherToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao remover o professor do curso", ex);
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

    public async Task<List<CourseTeacherResponseDTO>> GetTeachersByIdCourseAsync(Guid CursoId)
    {
        try
        {
            var course = await _context.Cursos.FirstOrDefaultAsync(c => c.Id == CursoId);
            if (course == null)
            {
                throw new ApplicationException("Curso não encontrado.");
            }

            var relatedTeachers = await _context.CursoProfessores.
            Where(cp => cp.Id_Curso == CursoId)
            .Select(cp => new CourseTeacherResponseDTO
            {
                Name = cp.Professor.Name,
                MiniResume = cp.Professor.MiniResume,
                ImagemUrl = cp.Professor.ImagemUrl
            })
            .ToListAsync();
            return relatedTeachers;

        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao listar os professores do curso", ex);
        }
    }

    public async Task<UpdateCourseDTO> UpdateCourseAsync(Guid id, UpdateCourseDTO dto)
    {
         try
        {
            var courseExisting = await _context.Cursos.FirstOrDefaultAsync(r => r.Id == id);

            if (courseExisting == null)
            {
                throw new ApplicationException("Curso não encontrada.");
            }

            courseExisting.Status = dto.Status;
            courseExisting.Name = dto.Name;
            courseExisting.Type = dto.Type;
            courseExisting.Presentation = dto.Presentation;
            courseExisting.StartForecast = dto.StartForecast;
            courseExisting.Modality = dto.Modality;
            courseExisting.Location = dto.Modality;
            courseExisting.Workload = dto.Workload;
            courseExisting.Duration = dto.Duration;
            courseExisting.Proposal = dto.Proposal;
            courseExisting.Requirements = dto.Requirements;
            courseExisting.Documentation = dto.Documentation;
            courseExisting.Faculty = dto.Faculty;
            courseExisting.Curriculum = dto.Curriculum;
            courseExisting.RegistrationPrice = dto.RegistrationPrice;
            courseExisting.MonthlyPrice = dto.MonthlyPrice;
            courseExisting.TotalPrice = dto.TotalPrice;
            courseExisting.Installments = dto.Installments;
            courseExisting.CashPrice = dto.CashPrice;
            courseExisting.FullPrice = dto.FullPrice;
            courseExisting.Discount = dto.Discount;
            courseExisting.ImagemUrl = dto.ImagemUrl;
            courseExisting.CoordenadorId = dto.Coordenador.Id;

            _context.Cursos.Update(courseExisting);
            await _context.SaveChangesAsync();
            return _mapper.Map<UpdateCourseDTO>(courseExisting);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao atualizar a matrícula", ex);
        }
    }
}
