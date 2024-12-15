using StudentEnrollmentSystem.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace StudentEnrollmentSystem.Domain.Validators
{
    public static class EnrollmentValidator
    {
        /// <summary>
        /// Validates an Enrollment object.
        /// Checks:
        /// - Valid (non-negative) Student ID
        /// - Valid (non-negative) Course ID
        /// - Valid (non-default) Enrollment date
        /// - Student is not already enrolled in the same course
        /// </summary>
        /// <param name="enrollment">The enrollment to validate.</param>
        /// <param name="existingEnrollments">A collection of existing enrollments to check for duplicates.</param>
        /// <param name="errorMessage">An error message if validation fails.</param>
        /// <returns>True if valid; otherwise false.</returns>
        public static bool ValidateEnrollment(Enrollment enrollment, IEnumerable<Enrollment> existingEnrollments, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (enrollment.StudentId <= 0)
            {
                errorMessage = "A valid Student ID is required.";
                return false;
            }

            if (enrollment.CourseId <= 0)
            {
                errorMessage = "A valid Course ID is required.";
                return false;
            }

            if (enrollment.EnrollmentDate == default)
            {
                errorMessage = "A valid enrollment date is required.";
                return false;
            }

            // Check that this student is not already enrolled in the same course
            bool alreadyEnrolled = existingEnrollments.Any(e =>
                e.StudentId == enrollment.StudentId && e.CourseId == enrollment.CourseId);

            if (alreadyEnrolled)
            {
                errorMessage = $"Student with ID {enrollment.StudentId} is already enrolled in course with ID {enrollment.CourseId}.";
                return false;
            }

            return true;
        }
    }
}
