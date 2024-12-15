using System;
using StudentEnrollmentSystem.Domain.Services;

namespace StudentEnrollmentSystem.Presentation
{
    public static class MainMenu
    {
        public static void Show(StudentService studentService, CourseService courseService, EnrollmentService enrollmentService)
        {
            while (true)
            {
                ShowHeader("Student Enrollment System");
                Console.WriteLine("1. Manage Students");
                Console.WriteLine("2. Manage Courses");
                Console.WriteLine("3. Manage Enrollments");
                Console.WriteLine("4. View Dashboard");
                Console.WriteLine("5. Help");
                Console.WriteLine("6. Exit");
                Console.WriteLine("========================================");
                Console.Write("Enter your choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        NavigateTo("Manage Students");
                        StudentMenu.Show(studentService);
                        break;
                    case "2":
                        NavigateTo("Manage Courses");
                        CourseMenu.Show(courseService);
                        break;
                    case "3":
                        NavigateTo("Manage Enrollments");
                        EnrollmentMenu.Show(enrollmentService, studentService, courseService);
                        break;
                    case "4":
                        ShowDashboard(studentService, courseService, enrollmentService);
                        break;
                    case "5":
                        ShowHelp();
                        break;
                    case "6":
                        ShowMessage("Exiting... Goodbye!", ConsoleColor.Green);
                        return;
                    default:
                        ShowMessage("Invalid choice. Press any key to try again...", ConsoleColor.Red);
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void ShowHeader(string title)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('=', 40));
            Console.WriteLine($"         {title.ToUpper()}         ");
            Console.WriteLine(new string('=', 40));
            Console.ResetColor();
        }

        private static void NavigateTo(string menuName)
        {
            Console.Clear();
            ShowMessage($"Navigating to {menuName}...", ConsoleColor.Gray);
        }

        private static void ShowMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private static void ShowDashboard(StudentService studentService, CourseService courseService, EnrollmentService enrollmentService)
        {
            try
            {
                Console.Clear();
                ShowHeader("Dashboard");

                // Fetching actual data from services
                var totalStudents = studentService.GetAllStudentsAsync().Result.Count;
                var totalCourses = courseService.GetAllCoursesAsync().Result.Count;
                var totalEnrollments = enrollmentService.GetAllEnrollmentsAsync().Result.Count;

                // Displaying the counts
                Console.WriteLine($"Total Students Registered: {totalStudents}");
                Console.WriteLine($"Total Courses Registered: {totalCourses}");
                Console.WriteLine($"Total Enrollments: {totalEnrollments}");
                Console.WriteLine("========================================");
                Console.WriteLine("Press any key to go back to the main menu...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: Could not load the dashboard. Details: {ex.Message}");
                Console.ResetColor();
                Console.ReadKey();
            }
        }

        private static void ShowHelp()
        {
            Console.Clear();
            ShowHeader("Help");
            Console.WriteLine("Here are the features of the Student Enrollment System:");
            Console.WriteLine("1. Manage Students: Add, list, search, edit, or delete student records.");
            Console.WriteLine("2. Manage Courses: Add, list, search, edit, or delete course records.");
            Console.WriteLine("3. Manage Enrollments: Enroll students in courses, list enrollments, or delete enrollments.");
            Console.WriteLine("4. View Dashboard: Displays an overview of the system, including total counts.");
            Console.WriteLine("5. Help: Displays this help menu.");
            Console.WriteLine("6. Exit: Exits the system.");
            Console.WriteLine("========================================");
            Console.WriteLine("Press any key to go back to the main menu...");
            Console.ReadKey();
        }
    }
}
