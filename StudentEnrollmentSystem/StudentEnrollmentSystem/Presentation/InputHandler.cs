using System;

namespace StudentEnrollmentSystem.Presentation
{
    public static class InputHandler
    {
        public static int GetInteger(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result))
                    return result;
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        public static string GetString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    return input;
                Console.WriteLine("Input cannot be empty. Please try again.");
            }
        }

        public static DateTime GetDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (DateTime.TryParse(Console.ReadLine(), out DateTime result))
                    return result;
                Console.WriteLine("Invalid date format. Please enter a valid date (e.g., YYYY-MM-DD).");
            }
        }
    }
}