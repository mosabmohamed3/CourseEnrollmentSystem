namespace CourseEnrollmentSystem.ViewModels;

public class EnrollmentListItemViewModel
{
    public int Id { get; set; }

    public string StudentName { get; set; } = string.Empty;

    public string CourseTitle { get; set; } = string.Empty;

    public DateTime EnrolledOn { get; set; }
}


