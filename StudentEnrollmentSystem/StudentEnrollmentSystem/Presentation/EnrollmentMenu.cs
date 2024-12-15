using System;
using System.Threading.Tasks;
using StudentEnrollmentSystem.Domain.Models;
using StudentEnrollmentSystem.Domain.Services;

namespace StudentEnrollmentSystem.Presentation
{
    public class EnrollmentMenu
    {
        private readonly EnrollmentService _enrollmentService;
        private readonly StudentService _studentService;
        private readonly CourseService _courseService;

        public EnrollmentMenu(EnrollmentService enrollmentService, StudentService studentService, CourseService courseService)
        {
            _enrollmentService = enrollmentService;
            _studentService = studentService;
            _courseService = courseService;
        }

        // Static method to serve as an entry point for the menu
        public static void Show(EnrollmentService enrollmentService, StudentService studentService, CourseService courseService)
        {
            var menu = new EnrollmentMenu(enrollmentService, studentService, courseService);
            menu.DisplayAsync().GetAwaiter().GetResult();
        }

        public async Task DisplayAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Manage Enrollments ===");
                Console.WriteLine("1. Enroll Student in Course");
                Console.WriteLine("2. View All Enrollments");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Enter your choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await EnrollStudentAsync();
                        break;
                    case "2":
                        await ViewAllEnrollmentsAsync();
                        break;
                    case "3":
                        return; // Exit the enrollment menu and return to the main menu
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Press Enter to try again.");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                }
            }
        }

        private async Task EnrollStudentAsync()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Enroll Student in Course ===");

                // Display all students
                var allStudents = await _studentService.GetAllStudentsAsync();
                if (allStudents.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("No students found. Please add students first.");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("Available Students:");
                foreach (var student in allStudents)
                {
                    Console.WriteLine($"ID: {student.Id}, Name: {student.FirstName} {student.LastName}");
                }

                var studentId = InputHandler.GetInteger("Enter Student ID: ");
                var selectedStudent = await _studentService.GetStudentByIdAsync(studentId);
                if (selectedStudent == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Student ID. Press Enter to continue...");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
                }

                // Display all courses
                var allCourses = await _courseService.GetAllCoursesAsync();
                if (allCourses.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("No courses found. Please add courses first.");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("\nAvailable Courses:");
                foreach (var course in allCourses)
                {
                    Console.WriteLine($"ID: {course.Id}, Name: {course.Name}, Capacity: {course.Capacity}");
                }

                var courseId = InputHandler.GetInteger("Enter Course ID: ");
                var selectedCourse = await _courseService.GetCourseByIdAsync(courseId);
                if (selectedCourse == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Course ID. Press Enter to continue...");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
                }

                if (selectedCourse.Capacity <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This course has no available capacity. Press Enter to continue...");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
                }

                // Enroll the student
                var enrollment = new Enrollment
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    EnrollmentDate = DateTime.Now
                };

                await _enrollmentService.AddEnrollmentAsync(enrollment);

                // Optionally decrement course capacity
                selectedCourse.Capacity--;
                await _courseService.UpdateCourseAsync(selectedCourse);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Student successfully enrolled in the course! Press Enter to continue...");
                Console.ResetColor();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: Could not enroll student. Details: {ex.Message}");
                Console.ResetColor();
                Console.ReadLine();
            }
        }

        private async Task ViewAllEnrollmentsAsync()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== All Enrollments ===");

                var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
                if (enrollments.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("No enrollments found.");
                    Console.ResetColor();
                }
                else
                {
                    foreach (var enrollment in enrollments)
                    {
                        Console.WriteLine($"Student: {enrollment.Student.FirstName} {enrollment.Student.LastName}, " +
                                          $"Course: {enrollment.Course.Name}, Date: {enrollment.EnrollmentDate.ToShortDateString()}");
                    }
                }

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: Could not retrieve enrollments. Details: {ex.Message}");
                Console.ResetColor();
                Console.ReadLine();
            }
        }
    }
}
