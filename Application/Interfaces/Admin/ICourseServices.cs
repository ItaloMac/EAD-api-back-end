using Application.DTOs.Admin.Course;

namespace Application.Interfaces.Admin;

public interface ICourseServices
{
    Task<List<CoursesReponseDTO>> GetAllCoursesAsync();
    Task<CoursesReponseDTO> GetCourseByIdAsync(Guid id);
    Task<CreateCourseDTO> CreateCourseAsync(CreateCourseDTO dto);
    Task<UpdateCourseDTO> UpdateCourseAsync(Guid id, UpdateCourseDTO dto);
    Task<bool> DeleteCourseAsync(Guid id);
    Task<List<CourseTeacherResponseDTO>> GetTeachersByIdCourseAsync(Guid id);
    Task<CourseTeacherDTO> AssignProfessorToCourseAsync(Guid CourseId, CourseTeacherDTO ProfessorId);
    Task<bool> DeleteTeacherFromCourseAsync(Guid CourseId, Guid ProfessorId);
    Task<AssignCordinatorDTO> AssignCordinatorAsync(Guid CourseId, AssignCordinatorDTO CoordenadorId);
    Task<List<CourseClassListDTO>> CourseClassListAssync(Guid CourseId);
}
