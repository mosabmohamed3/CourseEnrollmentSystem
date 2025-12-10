using CourseEnrollmentSystem.Helper.Extensions;
using CourseEnrollmentSystem.Services.Interfaces;
using CourseEnrollmentSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseEnrollmentSystem.Controllers;

public class CoursesController(ICourseService courseService) : Controller
{
    private readonly ICourseService _courseService = courseService;

    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
    {
        var result = await _courseService.GetPagedAsync(pageNumber, pageSize);
        return View(result.ToCourseListViewModel());
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var course = await _courseService.GetByIdAsync(id);
        if (course == null) return NotFound();

        return View(course.ToViewModelWithEnrollments());
    }

    [HttpGet]
    public IActionResult Create()
        => View(new CreateCourseViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCourseViewModel courseVM)
    {
        if (!ModelState.IsValid) return View(courseVM);

        var course = courseVM.ToEntity();
        var res = await _courseService.AddAsync(course);
        if (!res.IsValid)
        {
            foreach (var err in res.Errors)
                ModelState.AddModelError(err.FieldName, err.ErrorMessage);
            return View(courseVM);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var vm = await BuildCourseViewModelAsync(id);
        if (vm == null) return NotFound();
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, GetCourseViewModel courseVM)
    {
        if (id != courseVM.Id) return BadRequest();
        if (!ModelState.IsValid) return View(courseVM);

        var course = courseVM.ToEntity();
        var res = await _courseService.UpdateAsync(course);
        if (!res.IsValid)
        {
            foreach (var err in res.Errors)
                ModelState.AddModelError(err.FieldName, err.ErrorMessage);
            return View(courseVM);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var vm = await BuildCourseViewModelAsync(id);
        if (vm == null) return NotFound();
        return View(vm);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmDelete(int id)
    {
        await _courseService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task<GetCourseViewModel?> BuildCourseViewModelAsync(int id)
    {
        var course = await _courseService.GetByIdAsync(id);
        return course?.ToViewModel();
    }
}

