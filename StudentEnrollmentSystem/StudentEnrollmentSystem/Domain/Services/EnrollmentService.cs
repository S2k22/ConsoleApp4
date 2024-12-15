using StudentEnrollmentSystem.Data;
using StudentEnrollmentSystem.Domain.Models;
using StudentEnrollmentSystem.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollmentSystem.Domain.Services
{
    public class EnrollmentService
    {
        private readonly EnrollmentRepository _repository;

        public EnrollmentService(EnrollmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Enrollment>> GetAllEnrollmentsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Enrollment?> GetEnrollmentByIdAsync(int studentId, int courseId)
        {
            return await _repository.GetByIdAsync(studentId, courseId);
        }

        public async Task AddEnrollmentAsync(Enrollment enrollment)
        {
            // Fetch all existing enrollments for validation
            var existingEnrollments = await _repository.GetAllAsync();

            // Validate the enrollment
            if (!EnrollmentValidator.ValidateEnrollment(enrollment, existingEnrollments, out var errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            await _repository.AddAsync(enrollment);
        }

        public async Task DeleteEnrollmentAsync(int studentId, int courseId)
        {
            await _repository.DeleteAsync(studentId, courseId);
        }
    }
}
