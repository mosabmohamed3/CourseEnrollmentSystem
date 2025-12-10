namespace CourseEnrollmentSystem.ViewModels;

public class GetCourseViewModel : CreateCourseViewModel
{
    public int Id { get; set; }

    public int CurrentEnrollmentCount { get; set; }

    public int AvailableSlots { get; set; }

    public List<EnrollmentSummaryViewModel> Enrollments { get; set; } = new();
}


