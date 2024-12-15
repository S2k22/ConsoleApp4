using StudentEnrollmentSystem.Data;
using StudentEnrollmentSystem.Domain.Models;
using StudentEnrollmentSystem.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollmentSystem.Domain.Services
{
    public class CourseService
    {
        private readonly CourseRepository _repository;

        public CourseService(CourseRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddCourseAsync(Course course)
        {
            // Fetch all existing courses for validation
            var existingCourses = await _repository.GetAllAsync();

            // Validate course data
            if (!CourseValidator.ValidateCourse(course, existingCourses, out var errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            await _repository.AddAsync(course);
        }

        public async Task UpdateCourseAsync(Course course)
        {
            // Fetch all existing courses for validation
            var existingCourses = await _repository.GetAllAsync();

            // Validate course data
            if (!CourseValidator.ValidateCourse(course, existingCourses, out var errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            await _repository.UpdateAsync(course);
        }

        public async Task DeleteCourseAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
