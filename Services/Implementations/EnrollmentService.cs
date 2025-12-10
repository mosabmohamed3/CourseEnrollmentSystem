using CourseEnrollmentSystem.Data;
using CourseEnrollmentSystem.Helper.Common;
using CourseEnrollmentSystem.Models;
using CourseEnrollmentSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseEnrollmentSystem.Services.Implementations;

public class EnrollmentService(AppDbContext context) : IEnrollmentService
{
    private readonly AppDbContext _context = context;

    public async Task<List<Enrollment>> GetAllAsync()
    {
        return await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .OrderByDescending(e => e.EnrolledOn)
            .ToListAsync();
    }

    public async Task<Enrollment?> GetByIdAsync(int id)
    {
        return await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<BusinessValidationResult> EnrollAsync(int studentId, int courseId)
    {
        var validation = new BusinessValidationResult();

        var course = await _context.Courses
            .Include(c => c.Enrollments)
            .FirstOrDefaultAsync(c => c.Id == courseId);
        if (course == null)
        {
            validation.Errors.Add(new FieldError
            {
                FieldName = nameof(courseId),
                ErrorMessage = "Course not found"
            });
            return validation;
        }

        var studentExists = await _context.Students.AnyAsync(s => s.Id == studentId);
        if (!studentExists)
        {
            validation.Errors.Add(new FieldError
            {
                FieldName = nameof(studentId),
                ErrorMessage = "Student not found"
            });
            return validation;
        }

        var alreadyEnrolled = await _context.Enrollments
            .AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId);
        if (alreadyEnrolled)
        {
            validation.Errors.Add(new FieldError
            {
                FieldName = string.Empty,
                ErrorMessage = "Student is already enrolled in this course"
            });
            return validation;
        }

        var currentCount = course.Enrollments.Count;
        if (currentCount >= course.MaxCapacity)
        {
            validation.Errors.Add(new FieldError
            {
                FieldName = nameof(courseId),
                ErrorMessage = "Course is full; no available slots"
            });
            return validation;
        }

        var enrollment = new Enrollment
        {
            StudentId = studentId,
            CourseId = courseId,
            EnrolledOn = DateTime.UtcNow.Date
        };

        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        return validation;
    }

    public async Task DeleteAsync(int id)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);
        if (enrollment == null) return;
        _context.Enrollments.Remove(enrollment);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetAvailableSlotsAsync(int courseId)
    {
        var course = await _context.Courses
            .Include(c => c.Enrollments)
            .FirstOrDefaultAsync(c => c.Id == courseId);
        return course == null ? 0 : Math.Max(0, course.MaxCapacity - course.Enrollments.Count);
    }
}

