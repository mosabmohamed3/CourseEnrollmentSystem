using CourseEnrollmentSystem.Models;
using CourseEnrollmentSystem.Helper.Common;

namespace CourseEnrollmentSystem.Services.Interfaces;

public interface IEnrollmentService
{
    Task<List<Enrollment>> GetAllAsync();
    Task<Enrollment?> GetByIdAsync(int id);
    Task<BusinessValidationResult> EnrollAsync(int studentId, int courseId);
    Task DeleteAsync(int id);
    Task<int> GetAvailableSlotsAsync(int courseId);
}

