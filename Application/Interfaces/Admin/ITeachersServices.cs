using System;
using Application.DTOs.Admin.Teacher;

namespace Application.Interfaces.Admin;

public interface ITeacherServices
{
    Task<List<TeacherResponseDTO>> GetAllTeachers();
}
