using CourseEnrollmentSystem.Models;
using CourseEnrollmentSystem.Services.Interfaces;
using CourseEnrollmentSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseEnrollmentSystem.Controllers;

public class StudentsController(IStudentService studentService) : Controller
{
    private readonly IStudentService _studentService = studentService;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var students = await _studentService.GetAllAsync();
        
        var result = students.Select(s => new GetStudentViewModel
        {
            Id = s.Id,
            FullName = s.FullName,
            Email = s.Email,
            Birthdate = s.Birthdate,
            NationalId = s.NationalId,
            PhoneNumber = s.PhoneNumber
        }).ToList();

        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        if (student == null) return NotFound();

        var studentVM = new GetStudentViewModel
        {
            Id = student.Id,
            FullName = student.FullName,
            Email = student.Email,
            Birthdate = student.Birthdate,
            NationalId = student.NationalId,
            PhoneNumber = student.PhoneNumber
        };

        return View(studentVM);
    }

    [HttpGet]
    public IActionResult Create()
        => View(new CreateStudentViewModel { Birthdate = DateTime.UtcNow.AddYears(-18) });

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateStudentViewModel studentVM)
    {
        if (!ModelState.IsValid) return View(studentVM);

        var student = new Student
        {
            FullName = studentVM.FullName,
            Email = studentVM.Email,
            Birthdate = studentVM.Birthdate,
            NationalId = studentVM.NationalId,
            PhoneNumber = studentVM.PhoneNumber
        };

        var res = await _studentService.AddAsync(student);
        if (!res.IsValid)
        {
            foreach (var err in res.Errors)
                ModelState.AddModelError(err.FieldName, err.ErrorMessage);

            return View(studentVM);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var vm = await BuildStudentViewModelAsync(id);
        if (vm == null) return NotFound();
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(GetStudentViewModel studentVM)
    {
        if (!ModelState.IsValid) return View(studentVM);

        var student = new Student
        {
            Id = studentVM.Id,
            FullName = studentVM.FullName,
            Email = studentVM.Email,
            Birthdate = studentVM.Birthdate,
            NationalId = studentVM.NationalId,
            PhoneNumber = studentVM.PhoneNumber
        };

        var res = await _studentService.UpdateAsync(student);
        if (!res.IsValid)
        {
            foreach (var err in res.Errors)
                ModelState.AddModelError(err.FieldName, err.ErrorMessage);

            return View(studentVM);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var vm = await BuildStudentViewModelAsync(id);
        if (vm == null) return NotFound();
        return View(vm);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmDelete(int id)
    {
        await _studentService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task<GetStudentViewModel?> BuildStudentViewModelAsync(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        if (student == null) return null;

        return new GetStudentViewModel
        {
            Id = student.Id,
            FullName = student.FullName,
            Email = student.Email,
            Birthdate = student.Birthdate,
            NationalId = student.NationalId,
            PhoneNumber = student.PhoneNumber
        };
    }
}

