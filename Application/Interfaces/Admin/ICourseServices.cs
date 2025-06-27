using Application.DTOs.Admin.Course;

namespace Application.Interfaces.Admin;

public interface ICourseServices
{
    Task<List<CoursesReponseDTO>> GetAllCoursesAsync();
    Task<CoursesReponseDTO> GetCourseByIdAsync(Guid id);
    Task<CreateCourseDTO> CreateCourseAsync(CreateCourseDTO dto);
}
