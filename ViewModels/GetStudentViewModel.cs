using System.ComponentModel.DataAnnotations;

namespace CourseEnrollmentSystem.ViewModels;

public class GetStudentViewModel : CreateStudentViewModel
{
    [Required]
    public int Id { get; set; }
}
