using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CourseEnrollmentSystem.Models;

namespace CourseEnrollmentSystem.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
