using CourseEnrollmentSystem.Models;
using CourseEnrollmentSystem.ViewModels;

namespace CourseEnrollmentSystem.Helper.Extensions;

public static class EnrollmentExtensions
{
    public static EnrollmentListItemViewModel ToListItemViewModel(this Enrollment enrollment)
    {
        return new EnrollmentListItemViewModel
        {
            Id = enrollment.Id,
            StudentName = enrollment.Student?.FullName!,
            CourseTitle = enrollment.Course?.Title!,
            EnrolledOn = enrollment.EnrolledOn
        };
    }

    public static List<EnrollmentListItemViewModel> ToListItemViewModelList(
        this IEnumerable<Enrollment> enrollments
    )
    => enrollments.Select(e => e.ToListItemViewModel()).ToList();

    public static EnrollmentSummaryViewModel ToSummaryViewModel(this Enrollment enrollment)
    {
        return new EnrollmentSummaryViewModel
        {
            StudentName = enrollment.Student?.FullName!,
            EnrolledOn = enrollment.EnrolledOn
        };
    }
}

