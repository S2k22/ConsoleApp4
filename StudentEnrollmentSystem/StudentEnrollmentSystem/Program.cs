using System;
using Microsoft.EntityFrameworkCore;
using StudentEnrollmentSystem.Data;
using StudentEnrollmentSystem.Domain.Services;
using StudentEnrollmentSystem.Presentation;

namespace StudentEnrollmentSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Initialize dependencies
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlite("Data Source=StudentEnrollment.db") // Ensure SQLite is installed
                .Options;

            var dbContext = new DatabaseContext(options);

            // Ensure the database is created
            dbContext.Database.EnsureCreated();

            // Initialize repositories
            var studentRepository = new StudentRepository(dbContext);
            var courseRepository = new CourseRepository(dbContext);
            var enrollmentRepository = new EnrollmentRepository(dbContext);

            // Initialize services
            var studentService = new StudentService(studentRepository);
            var courseService = new CourseService(courseRepository);
            var enrollmentService = new EnrollmentService(enrollmentRepository);

            // Start the main menu
            MainMenu.Show(studentService, courseService, enrollmentService);
        }
        
    }
}