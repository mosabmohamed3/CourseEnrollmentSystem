using System.ComponentModel.DataAnnotations;

namespace CourseEnrollmentSystem.ViewModels;

public class CreateStudentViewModel
{
    [Required, StringLength(150)]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, DataType(DataType.Date)]
    public DateTime Birthdate { get; set; }

    [Required, StringLength(14)]
    public string NationalId { get; set; } = string.Empty;

    [StringLength(11)]
    public string? PhoneNumber { get; set; }
}
