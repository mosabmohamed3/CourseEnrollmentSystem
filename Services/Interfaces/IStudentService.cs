using CourseEnrollmentSystem.Helper;
using CourseEnrollmentSystem.Models;

namespace CourseEnrollmentSystem.Services.Interfaces;

public interface IStudentService
{
    Task<List<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(int id);
    Task<BusinessValidationResult> AddAsync(Student student);
    Task<BusinessValidationResult> UpdateAsync(Student student);
    Task DeleteAsync(int id);
    Task<bool> EmailExistsAsync(string email, int? ignoreId = null);
    Task<bool> NationalIdExistsAsync(string nationalId, int? ignoreId = null);
}

