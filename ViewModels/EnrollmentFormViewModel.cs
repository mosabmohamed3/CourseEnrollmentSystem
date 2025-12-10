using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseEnrollmentSystem.ViewModels;

public class EnrollmentFormViewModel
{
    [Required]
    [Display(Name = "Student")]
    public int StudentId { get; set; }

    [Required]
    [Display(Name = "Course")]
    public int CourseId { get; set; }

    public IEnumerable<SelectListItem> Students { get; set; } = Enumerable.Empty<SelectListItem>();
    public IEnumerable<SelectListItem> Courses { get; set; } = Enumerable.Empty<SelectListItem>();

    public int AvailableSlots { get; set; }
}

