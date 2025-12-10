using CourseEnrollmentSystem.Data;
using CourseEnrollmentSystem.Helper.Common;
using CourseEnrollmentSystem.Models;
using CourseEnrollmentSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseEnrollmentSystem.Services.Implementations;

public class StudentService(AppDbContext context) : IStudentService
{
    private readonly AppDbContext _context = context;

    public async Task<List<Student>> GetAllAsync()
    {
        return await _context.Students
            .OrderBy(s => s.FullName)
            .ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _context.Students
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<BusinessValidationResult> AddAsync(Student student)
    {
        var result = new BusinessValidationResult();

        if (await EmailExistsAsync(student.Email))
            result.Errors.Add(new FieldError
            {
                FieldName = nameof(student.Email),
                ErrorMessage = "Email is already in use"
            });

        if (await NationalIdExistsAsync(student.NationalId))
            result.Errors.Add(new FieldError
            {
                FieldName = nameof(student.NationalId),
                ErrorMessage = "National ID is already in use"
            });

        if (!result.IsValid)
            return result;

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return result;
    }

    public async Task<BusinessValidationResult> UpdateAsync(Student student)
    {
        var result = new BusinessValidationResult();

        if (await EmailExistsAsync(student.Email, student.Id))
            result.Errors.Add(new FieldError
            {
                FieldName = nameof(student.Email),
                ErrorMessage = "Email is already in use"
            });

        if (await NationalIdExistsAsync(student.NationalId, student.Id))
            result.Errors.Add(new FieldError
            {
                FieldName = nameof(student.NationalId),
                ErrorMessage = "National ID is already in use"
            });

        if (!result.IsValid)
            return result;

        _context.Students.Update(student);
        await _context.SaveChangesAsync();

        return result;
    }

    public async Task DeleteAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return;
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
    }

    private async Task<bool> EmailExistsAsync(string email, int? ignoreId = null)
        => await _context.Students
            .AnyAsync(s => s.Email == email && (!ignoreId.HasValue || s.Id != ignoreId.Value));

    private async Task<bool> NationalIdExistsAsync(string nationalId, int? ignoreId = null)
        => await _context.Students
            .AnyAsync(s => s.NationalId == nationalId && (!ignoreId.HasValue || s.Id != ignoreId.Value));
}

