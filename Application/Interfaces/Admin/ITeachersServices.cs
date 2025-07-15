using System;
using Application.DTOs.Admin.Course;
using Application.DTOs.Admin.Teacher;

namespace Application.Interfaces.Admin;

public interface ITeacherServices
{
    Task<List<TeacherResponseDTO>> GetAllTeachers();
    Task<TeacherResponseDTO> GetTeacherByID(Guid id);
    Task<CreateTeacherDTO> CreateTeacherAsync(CreateTeacherDTO dto);
    Task<UpdateTeacherDTO> UpdateTeacherAsync(Guid id, UpdateTeacherDTO dto);
    Task<bool> DeleteTeacherAsync(Guid id);
    Task<List<TeacherCourseResponseDTO>> GetCoursesByIdTeacherAsync(Guid id);

}
