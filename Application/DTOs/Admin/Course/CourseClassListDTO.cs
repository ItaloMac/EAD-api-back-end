namespace Application.DTOs.Admin.Course;

public class CourseClassListDTO
{
    public required string Name { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
}
