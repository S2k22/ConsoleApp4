using StudentEnrollmentSystem.Domain.Models;

namespace StudentEnrollmentSystem.Domain.Validators
{
    public class StudentValidator
    {
        public static bool ValidateStudent(Student student, out string errorMessage)
        {
            errorMessage = string.Empty;

            // First name validation
            if (string.IsNullOrWhiteSpace(student.FirstName))
            {
                errorMessage = "First name is required.";
                return false;
            }
            if (ContainsNumbers(student.FirstName))
            {
                errorMessage = "First name cannot contain numbers.";
                return false;
            }

            // Last name validation
            if (string.IsNullOrWhiteSpace(student.LastName))
            {
                errorMessage = "Last name is required.";
                return false;
            }
            if (ContainsNumbers(student.LastName))
            {
                errorMessage = "Last name cannot contain numbers.";
                return false;
            }

            // Date of birth validation
            if (student.DateOfBirth == default)
            {
                errorMessage = "Valid date of birth is required.";
                return false;
            }

            // Email validation
            if (string.IsNullOrWhiteSpace(student.Email) || !IsValidEmail(student.Email))
            {
                errorMessage = "A valid email address is required.";
                return false;
            }

            return true;
        }

        // Helper method to validate email
        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Helper method to check if a string contains numbers
        private static bool ContainsNumbers(string input)
        {
            return input.Any(char.IsDigit);
        }
    }
}
