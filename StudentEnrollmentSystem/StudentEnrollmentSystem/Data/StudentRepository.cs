using Microsoft.EntityFrameworkCore;
using StudentEnrollmentSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentEnrollmentSystem.Data
{
    public class StudentRepository
    {
        private readonly DatabaseContext _context;

        public StudentRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            try
            {
                return await _context.Students.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all students.", ex);
            }
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Students.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching the student with ID: {id}.", ex);
            }
        }

        public async Task AddAsync(Student student)
        {
            try
            {
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a new student.", ex);
            }
        }

        public async Task UpdateAsync(Student student)
        {
            try
            {
                _context.Students.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the student with ID: {student.Id}.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);
                if (student != null)
                {
                    _context.Students.Remove(student);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the student with ID: {id}.", ex);
            }
        }
    }
}





