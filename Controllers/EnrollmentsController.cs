using CourseEnrollmentSystem.Helper.Extensions;
using CourseEnrollmentSystem.Services.Interfaces;
using CourseEnrollmentSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseEnrollmentSystem.Controllers;

public class EnrollmentsController(IEnrollmentService enrollmentService, IStudentService studentService, ICourseService courseService) 
    : Controller
{
    private readonly IEnrollmentService _enrollmentService = enrollmentService;
    private readonly IStudentService _studentService = studentService;
    private readonly ICourseService _courseService = courseService;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var enrollments = await _enrollmentService.GetAllAsync();
        return View(enrollments.ToListItemViewModelList());
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var vm = await BuildEnrollmentFormViewModel();
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EnrollmentFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model = await BuildEnrollmentFormViewModel(model.StudentId, model.CourseId);
            return View(model);
        }

        var result = await _enrollmentService.EnrollAsync(model.StudentId, model.CourseId);
        if (!result.IsValid)
        {
            foreach (var err in result.Errors)
                ModelState.AddModelError(err.FieldName, err.ErrorMessage);
            model = await BuildEnrollmentFormViewModel(model.StudentId, model.CourseId);
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> AvailableSlots(int courseId)
    {
        var slots = await _enrollmentService.GetAvailableSlotsAsync(courseId);
        return Json(new { availableSlots = slots });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var enrollment = await _enrollmentService.GetByIdAsync(id);
        if (enrollment == null) return NotFound();
        return View(enrollment.ToListItemViewModel());
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmDelete(int id)
    {
        await _enrollmentService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task<EnrollmentFormViewModel> BuildEnrollmentFormViewModel(int? selectedStudent = null, int? selectedCourse = null)
    {
        var students = await _studentService.GetAllAsync();
        var courses = await _courseService.GetAllAsync(page: 1, pageSize: int.MaxValue);

        var courseId = selectedCourse ?? courses.FirstOrDefault()?.Id ?? 0;
        var slots = courseId == 0 ? 0 : await _enrollmentService.GetAvailableSlotsAsync(courseId);

        return new EnrollmentFormViewModel
        {
            StudentId = selectedStudent ?? 0,
            CourseId = courseId,
            AvailableSlots = slots,
            Students = students.Select(s => new SelectListItem
            {
                Text = s.FullName,
                Value = s.Id.ToString(),
                Selected = selectedStudent.HasValue && s.Id == selectedStudent.Value
            }),
            Courses = courses.Select(c => new SelectListItem
            {
                Text = $"{c.Title} (Capacity: {c.MaxCapacity})",
                Value = c.Id.ToString(),
                Selected = selectedCourse.HasValue && c.Id == selectedCourse.Value
            })
        };
    }
}

