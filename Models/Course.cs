using System.ComponentModel.DataAnnotations;

namespace CourseEnrollmentSystem.Models;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int MaxCapacity { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    
    public int CurrentEnrollmentCount => Enrollments?.Count ?? 0;
    public int AvailableSlots => Math.Max(0, MaxCapacity - CurrentEnrollmentCount);
}

