# Course Enrollment System

A course enrollment system built with ASP.NET Core MVC and Entity Framework Core using an InMemory database. The application follows a layered architecture with a dedicated service layer to isolate business logic.
## Features
- Student Management: Create, edit, delete, and view students.
- Course Management: Create, edit, delete, view courses, with pagination support.
- Course Enrollment: Enroll students in courses with validation for seat availability and prevention of duplicate enrollments.
- Dynamic Seat Availability: Enrollment form dynamically updates available seats using jQuery.
- Layered Architecture:
  - Lightweight Controllers
  - Service layer for Student, Course, and Enrollment operations
- Seed Data: Initial data for students and courses.

## Requirements
- .NET 8 SDK

## Running the Application
```bash
dotnet run
```
Then open your browser at: `http://localhost:5062`.

## Project Structure
- `Data/`: `AppDbContext`, model configuration, and seed data
- `Models/`: Entities (Student, Course, Enrollment)
- `Services/Interfaces`, `Services/Implementations`: Business logic layer.
- `Controllers/`: Thin controllers that call the service layer
- `ViewModels/`: View models for UI (e.g., CourseListViewModel, EnrollmentFormViewModel)
- `Views/`: Razor UI, including reusable partial views for students and courses

## Notes
- The InMemory database is initialized on application startup (EnsureCreated).
- All data is lost when the application stops, since it is not persisted.
