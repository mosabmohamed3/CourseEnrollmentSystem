using CourseEnrollmentSystem.Data;
using CourseEnrollmentSystem.Helper;
using CourseEnrollmentSystem.Helper.Common;
using CourseEnrollmentSystem.Models;
using CourseEnrollmentSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseEnrollmentSystem.Services.Implementations;

public class CourseService(AppDbContext context) : ICourseService
{
    private readonly AppDbContext _context = context;

    public async Task<List<Course>> GetAllAsync(int page = 1, int pageSize = 10)
    {
        return await _context.Courses
            .Include(c => c.Enrollments)
            .OrderBy(c => c.Title)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<PagedResult<Course>> GetPagedAsync(int pageNumber, int pageSize)
    {
        var query = _context.Courses
            .Include(c => c.Enrollments)
            .OrderBy(c => c.Title);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Course>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task<Course?> GetByIdAsync(int id)
    {
        return await _context.Courses
            .Include(c => c.Enrollments)
            .ThenInclude(e => e.Student)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<BusinessValidationResult> AddAsync(Course course)
    {
        var validation = await ValidateAsync(course);
        if (!validation.IsValid) return validation;

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        return validation;
    }

    public async Task<BusinessValidationResult> UpdateAsync(Course course)
    {
        var validation = await ValidateAsync(course, course.Id);
        if (!validation.IsValid) return validation;

        _context.Courses.Update(course);
        await _context.SaveChangesAsync();

        return validation;
    }

    public async Task DeleteAsync(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null) return;
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
    }

    private async Task<BusinessValidationResult> ValidateAsync(Course course, int? ignoreId = null)
    {
        var result = new BusinessValidationResult();

        var exists = await _context.Courses
            .AnyAsync(c => c.Title == course.Title && (!ignoreId.HasValue || c.Id != ignoreId.Value));
        if (exists)
        {
            result.Errors.Add(new FieldError
            {
                FieldName = nameof(course.Title),
                ErrorMessage = "Course title is already in use"
            });
        }

        if (course.MaxCapacity <= 0)
        {
            result.Errors.Add(new FieldError
            {
                FieldName = nameof(course.MaxCapacity),
                ErrorMessage = "Max capacity must be greater than zero"
            });
        }

        return result;
    }
}

