using CourseEnrollmentSystem.Models;

namespace CourseEnrollmentSystem.ViewModels;

public class CourseListViewModel
{
    public List<GetCourseViewModel> Courses { get; set; } = new();
    public int Page { get; set; }
    public int TotalPages { get; set; }
}

