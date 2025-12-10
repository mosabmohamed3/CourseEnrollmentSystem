using CourseEnrollmentSystem.Models;
using CourseEnrollmentSystem.ViewModels;

namespace CourseEnrollmentSystem.Helper.Extensions;

public static class StudentExtensions
{
    public static GetStudentViewModel ToViewModel(this Student student)
    {
        return new GetStudentViewModel
        {
            Id = student.Id,
            FullName = student.FullName,
            Email = student.Email,
            Birthdate = student.Birthdate,
            NationalId = student.NationalId,
            PhoneNumber = student.PhoneNumber
        };
    }

    public static List<GetStudentViewModel> ToViewModelList(this IEnumerable<Student> students)
        => students.Select(s => s.ToViewModel()).ToList();

    public static Student ToEntity(this CreateStudentViewModel viewModel)
    {
        return new Student
        {
            FullName = viewModel.FullName,
            Email = viewModel.Email,
            Birthdate = viewModel.Birthdate,
            NationalId = viewModel.NationalId,
            PhoneNumber = viewModel.PhoneNumber
        };
    }

    public static Student ToEntity(this GetStudentViewModel viewModel)
    {
        return new Student
        {
            Id = viewModel.Id,
            FullName = viewModel.FullName,
            Email = viewModel.Email,
            Birthdate = viewModel.Birthdate,
            NationalId = viewModel.NationalId,
            PhoneNumber = viewModel.PhoneNumber
        };
    }
}

