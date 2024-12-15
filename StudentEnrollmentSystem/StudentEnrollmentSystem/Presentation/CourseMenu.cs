using System;
using System.Threading.Tasks;
using StudentEnrollmentSystem.Domain.Models;
using StudentEnrollmentSystem.Domain.Services;

namespace StudentEnrollmentSystem.Presentation
{
    public class CourseMenu
    {
        private readonly CourseService _courseService;

        public CourseMenu(CourseService courseService)
        {
            _courseService = courseService;
        }

        // Static method to serve as an entry point for the menu
        public static void Show(CourseService courseService)
        {
            var menu = new CourseMenu(courseService);
            menu.DisplayAsync().GetAwaiter().GetResult();
        }

        public async Task DisplayAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Manage Courses ===");
                Console.WriteLine("1. Add Course");
                Console.WriteLine("2. View All Courses");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Enter your choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddCourseAsync();
                        break;
                    case "2":
                        await ViewAllCoursesAsync();
                        break;
                    case "3":
                        return; // Exit the course menu and return to the main menu
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Press Enter to try again.");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                }
            }
        }

        private async Task AddCourseAsync()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Add New Course ===");
                var name = InputHandler.GetString("Enter Course Name: ");
                var description = InputHandler.GetString("Enter Course Description: ");
                var capacity = InputHandler.GetInteger("Enter Course Capacity: ");

                var course = new Course
                {
                    Name = name,
                    Description = description,
                    Capacity = capacity
                };

                await _courseService.AddCourseAsync(course);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Course added successfully! Press Enter to continue...");
                Console.ResetColor();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: Could not add the course. Details: {ex.Message}");
                Console.ResetColor();
                Console.ReadLine();
            }
        }

        private async Task ViewAllCoursesAsync()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== All Courses ===");

                var courses = await _courseService.GetAllCoursesAsync();

                if (courses.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("No courses found.");
                    Console.ResetColor();
                }
                else
                {
                    foreach (var course in courses)
                    {
                        Console.WriteLine($"ID: {course.Id}, Name: {course.Name}, Capacity: {course.Capacity}");
                    }
                }

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: Could not retrieve courses. Details: {ex.Message}");
                Console.ResetColor();
                Console.ReadLine();
            }
        }
    }
}
