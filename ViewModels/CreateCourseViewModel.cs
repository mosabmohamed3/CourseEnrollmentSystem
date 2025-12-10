using System.ComponentModel.DataAnnotations;

namespace CourseEnrollmentSystem.ViewModels;

public class CreateCourseViewModel
{
    [Required, StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Required, Range(1, 1000)]
    public int MaxCapacity { get; set; }
}


