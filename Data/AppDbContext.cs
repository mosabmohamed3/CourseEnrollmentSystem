using Microsoft.EntityFrameworkCore;
using CourseEnrollmentSystem.Models;

namespace CourseEnrollmentSystem.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Student>()
            .HasIndex(s => s.Email)
            .IsUnique();

        modelBuilder.Entity<Student>()
            .Property(s => s.Email)
            .IsRequired();

        modelBuilder.Entity<Student>()
            .Property(s => s.NationalId)
            .IsRequired()
            .HasMaxLength(14);

        modelBuilder.Entity<Student>()
            .Property(s => s.PhoneNumber)
            .HasMaxLength(11);

        modelBuilder.Entity<Student>()
            .Property(s => s.Birthdate)
            .IsRequired();

        modelBuilder.Entity<Student>()
            .Property(s => s.FullName)
            .IsRequired()
            .HasMaxLength(150);

        modelBuilder.Entity<Enrollment>()
            .HasIndex(e => new { e.StudentId, e.CourseId })
            .IsUnique();

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                Id = 1,
                FullName = "John Doe",
                Email = "john.doe@example.com",
                Birthdate = new DateTime(1995, 5, 15),
                NationalId = "12345678901234",
                PhoneNumber = "1234567890"
            },
            new Student
            {
                Id = 2,
                FullName = "Jane Smith",
                Email = "jane.smith@example.com",
                Birthdate = new DateTime(1998, 8, 22),
                NationalId = "23456789012345",
                PhoneNumber = "0987654321"
            }
        );
        
        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                Id = 1,
                Title = "Introduction to Programming",
                Description = "Learn the basics of programming with C#",
                MaxCapacity = 30
            },
            new Course
            {
                Id = 2,
                Title = "Web Development with ASP.NET",
                Description = "Build web applications using ASP.NET MVC",
                MaxCapacity = 25
            },
            new Course
            {
                Id = 3,
                Title = "Database Design",
                Description = "Learn relational database concepts",
                MaxCapacity = 20
            }
        );
    }
}