using CourseEnrollmentSystem.Models;
using CourseEnrollmentSystem.ViewModels;
using CourseEnrollmentSystem.Helper;

namespace CourseEnrollmentSystem.Services.Interfaces;

public interface ICourseService
{
    Task<List<Course>> GetAllAsync(int page = 1, int pageSize = 10);
    Task<Course?> GetByIdAsync(int id);
    Task<BusinessValidationResult> AddAsync(Course course);
    Task<BusinessValidationResult> UpdateAsync(Course course);
    Task DeleteAsync(int id);
    Task<PagedResult<Course>> GetPagedAsync(int page, int pageSize);
}

