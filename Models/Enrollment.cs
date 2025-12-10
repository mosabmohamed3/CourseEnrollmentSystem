using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseEnrollmentSystem.Models;

public class Enrollment
{
    public int Id { get; set; }
    public DateTime EnrolledOn { get; set; } = DateTime.UtcNow.Date;

    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public Student? Student { get; set; }
    public Course? Course { get; set; }
}

