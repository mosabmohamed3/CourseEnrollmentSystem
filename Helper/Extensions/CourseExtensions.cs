using CourseEnrollmentSystem.Models;
using CourseEnrollmentSystem.ViewModels;

namespace CourseEnrollmentSystem.Helper.Extensions;

public static class CourseExtensions
{
    public static GetCourseViewModel ToViewModel(this Course course)
    {
        return new GetCourseViewModel
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            MaxCapacity = course.MaxCapacity,
            CurrentEnrollmentCount = course.Enrollments.Count,
            AvailableSlots = course.AvailableSlots
        };
    }

    public static GetCourseViewModel ToViewModelWithEnrollments(this Course course)
    {
        return new GetCourseViewModel
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            MaxCapacity = course.MaxCapacity,
            CurrentEnrollmentCount = course.Enrollments.Count,
            AvailableSlots = course.AvailableSlots,
            Enrollments = course.Enrollments
                .Select(e => new EnrollmentSummaryViewModel
                {
                    StudentName = e.Student?.FullName!,
                    EnrolledOn = e.EnrolledOn
                })
                .ToList()
        };
    }

    public static List<GetCourseViewModel> ToViewModelList(this IEnumerable<Course> courses)
        => courses.Select(c => c.ToViewModel()).ToList();

    public static Course ToEntity(this CreateCourseViewModel viewModel)
    {
        return new Course
        {
            Title = viewModel.Title,
            Description = viewModel.Description,
            MaxCapacity = viewModel.MaxCapacity
        };
    }

    public static Course ToEntity(this GetCourseViewModel viewModel)
    {
        return new Course
        {
            Id = viewModel.Id,
            Title = viewModel.Title,
            Description = viewModel.Description,
            MaxCapacity = viewModel.MaxCapacity
        };
    }

    public static CourseListViewModel ToCourseListViewModel(this PagedResult<Course> pagedResult)
    {
        return new CourseListViewModel
        {
            Courses = pagedResult.Items.ToViewModelList(),
            Page = pagedResult.PageNumber,
            TotalPages = pagedResult.TotalPages
        };
    }
}

