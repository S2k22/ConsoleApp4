using System;
using System.Threading.Tasks;
using StudentEnrollmentSystem.Domain.Models;
using StudentEnrollmentSystem.Domain.Services;

namespace StudentEnrollmentSystem.Presentation
{
    public class StudentMenu
    {
        private readonly StudentService _studentService;

        public StudentMenu(StudentService studentService)
        {
            _studentService = studentService;
        }

        // The new Show method serves as an entry point for the menu.
        public static void Show(StudentService studentService)
        {
            var menu = new StudentMenu(studentService);
            menu.DisplayAsync().GetAwaiter().GetResult();
        }

        public async Task DisplayAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Manage Students ===");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View All Students");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Enter your choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddStudentAsync();
                        break;
                    case "2":
                        await ViewAllStudentsAsync();
                        break;
                    case "3":
                        return; // Exit the student menu and return to the main menu
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Press Enter to try again.");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                }
            }
        }

        private async Task AddStudentAsync()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Add New Student ===");
                var firstName = InputHandler.GetString("Enter First Name: ");
                var lastName = InputHandler.GetString("Enter Last Name: ");
                var dateOfBirth = InputHandler.GetDate("Enter Date of Birth (YYYY-MM-DD): ");
                var email = InputHandler.GetString("Enter Email: ");

                var student = new Student
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = dateOfBirth,
                    Email = email
                };

                await _studentService.AddStudentAsync(student);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Student added successfully! Press Enter to continue...");
                Console.ResetColor();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: Could not add the student. Details: {ex.Message}");
                Console.ResetColor();
                Console.ReadLine();
            }
        }

        private async Task ViewAllStudentsAsync()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== All Students ===");

                var students = await _studentService.GetAllStudentsAsync();

                if (students.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("No students found.");
                    Console.ResetColor();
                }
                else
                {
                    foreach (var student in students)
                    {
                        Console.WriteLine($"ID: {student.Id}, Name: {student.FirstName} {student.LastName}, Email: {student.Email}");
                    }
                }

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: Could not retrieve students. Details: {ex.Message}");
                Console.ResetColor();
                Console.ReadLine();
            }
        }
    }
}
