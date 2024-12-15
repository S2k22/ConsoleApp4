using Microsoft.EntityFrameworkCore;
using StudentEnrollmentSystem.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentEnrollmentSystem.Data
{
    public class EnrollmentRepository
    {
        private readonly DatabaseContext _context;

        public EnrollmentRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Enrollment>> GetAllAsync()
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();
        }

        public async Task<Enrollment?> GetByIdAsync(int studentId, int courseId)
        {
            return await _context.Enrollments.FindAsync(studentId, courseId);
        }

        public async Task AddAsync(Enrollment enrollment)
        {
            await _context.Enrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int studentId, int courseId)
        {
            var enrollment = await _context.Enrollments.FindAsync(studentId, courseId);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
                await _context.SaveChangesAsync();
            }
        }
    }
}




