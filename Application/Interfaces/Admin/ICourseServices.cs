using System;
using Application.DTOs.Admin.Course;

namespace Application.Interfaces.Admin;

public interface ICourseServices
{
    Task<List<CoursesReponseDTO>> GetAllCoursesAsync();
}
