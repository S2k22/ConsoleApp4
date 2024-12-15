using StudentEnrollmentSystem.Data;
using StudentEnrollmentSystem.Domain.Models;
using StudentEnrollmentSystem.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentEnrollmentSystem.Domain.Services
{
    public class StudentService
    {
        private readonly StudentRepository _repository;

        public StudentService(StudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddStudentAsync(Student student)
        {
            // Validate the student data before adding
            if (!StudentValidator.ValidateStudent(student, out var errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            await _repository.AddAsync(student);
        }

        public async Task UpdateStudentAsync(Student student)
        {
            // Validate the student data before updating
            if (!StudentValidator.ValidateStudent(student, out var errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            await _repository.UpdateAsync(student);
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
