using CourseEnrollmentSystem.Helper.Common;
using CourseEnrollmentSystem.Models;

namespace CourseEnrollmentSystem.Services.Interfaces;

public interface IStudentService
{
    Task<List<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(int id);
    Task<BusinessValidationResult> AddAsync(Student student);
    Task<BusinessValidationResult> UpdateAsync(Student student);
    Task DeleteAsync(int id);
}

