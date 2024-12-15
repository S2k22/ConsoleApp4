using StudentEnrollmentSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentEnrollmentSystem.Domain.Validators
{
    public static class CourseValidator
    {
        /// <summary>
        /// Validates a given Course object.
        /// Checks:
        /// - Name is required and max length of 50
        /// - Capacity must be greater than 0
        /// - Description is required and max length of 150
        /// - Name does not duplicate an existing course (by name)
        /// </summary>
        /// <param name="course">The course to validate</param>
        /// <param name="existingCourses">A collection of existing courses to check duplicates against</param>
        /// <param name="errorMessage">An error message if validation fails</param>
        /// <returns>True if valid; otherwise false</returns>
        public static bool ValidateCourse(Course course, IEnumerable<Course> existingCourses, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Check required name
            if (string.IsNullOrWhiteSpace(course.Name))
            {
                errorMessage = "Course name is required.";
                return false;
            }

            // Check name length
            if (course.Name.Length > 50)
            {
                errorMessage = "Course name cannot exceed 50 characters.";
                return false;
            }

            // Check capacity
            if (course.Capacity <= 0)
            {
                errorMessage = "Course capacity must be greater than 0.";
                return false;
            }

            // Check required description
            if (string.IsNullOrWhiteSpace(course.Description))
            {
                errorMessage = "Course description is required.";
                return false;
            }

            // Check description length
            if (course.Description.Length > 150)
            {
                errorMessage = "Course description cannot exceed 150 characters.";
                return false;
            }

            // Check for duplicates by name (ignoring case)
            // Assuming that the uniqueness check should exclude the current course itself (if it has an Id).
            if (existingCourses.Any(c => c.Id != course.Id &&
                                         c.Name.Equals(course.Name, StringComparison.OrdinalIgnoreCase)))
            {
                errorMessage = $"A course named '{course.Name}' already exists.";
                return false;
            }

            return true;
        }
    }
}
