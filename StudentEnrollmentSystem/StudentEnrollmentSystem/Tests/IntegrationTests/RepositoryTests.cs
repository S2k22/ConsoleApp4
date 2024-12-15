using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using StudentEnrollmentSystem.Data;
using StudentEnrollmentSystem.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace StudentEnrollmentSystem.Tests.IntegrationTests
{
    [TestClass]
    public class RepositoryTests
    {
        private DatabaseContext _context;
        private StudentRepository _studentRepository;
        private CourseRepository _courseRepository;
        private EnrollmentRepository _enrollmentRepository;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _context = new DatabaseContext(options);
            _studentRepository = new StudentRepository(_context);
            _courseRepository = new CourseRepository(_context);
            _enrollmentRepository = new EnrollmentRepository(_context);
        }

        [TestCleanup]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        // StudentRepository Tests
        [TestMethod]
        public async Task StudentRepository_AddAsync_ShouldAddStudent()
        {
            var student = new Student
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new System.DateTime(2000, 1, 1),
                Email = "john.doe@example.com"
            };

            await _studentRepository.AddAsync(student);

            var result = await _context.Students.FirstOrDefaultAsync(s => s.Email == "john.doe@example.com");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task StudentRepository_GetAllAsync_ShouldReturnAllStudents()
        {
            await _context.Students.AddRangeAsync(new List<Student>
            {
                new Student { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new Student { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" }
            });
            await _context.SaveChangesAsync();

            var students = await _studentRepository.GetAllAsync();

            Assert.AreEqual(2, students.Count);
        }

        [TestMethod]
        public async Task StudentRepository_DeleteAsync_ShouldRemoveStudent()
        {
            var student = new Student
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            await _studentRepository.DeleteAsync(student.Id);

            var result = await _context.Students.FindAsync(student.Id);
            Assert.IsNull(result);
        }

        // CourseRepository Tests
        [TestMethod]
        public async Task CourseRepository_AddAsync_ShouldAddCourse()
        {
            var course = new Course
            {
                Name = "Math",
                Description = "Basic Math Course",
                Capacity = 30
            };

            await _courseRepository.AddAsync(course);

            var result = await _context.Courses.FirstOrDefaultAsync(c => c.Name == "Math");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CourseRepository_GetAllAsync_ShouldReturnAllCourses()
        {
            await _context.Courses.AddRangeAsync(new List<Course>
            {
                new Course { Name = "Math", Description = "Basic Math Course", Capacity = 30 },
                new Course { Name = "Science", Description = "Physics and Chemistry", Capacity = 20 }
            });
            await _context.SaveChangesAsync();

            var courses = await _courseRepository.GetAllAsync();

            Assert.AreEqual(2, courses.Count);
        }

        [TestMethod]
        public async Task CourseRepository_DeleteAsync_ShouldRemoveCourse()
        {
            var course = new Course
            {
                Name = "Math",
                Description = "Basic Math Course",
                Capacity = 30
            };
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            await _courseRepository.DeleteAsync(course.Id);

            var result = await _context.Courses.FindAsync(course.Id);
            Assert.IsNull(result);
        }

        // EnrollmentRepository Tests
        [TestMethod]
        public async Task EnrollmentRepository_AddAsync_ShouldAddEnrollment()
        {
            var student = new Student
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            var course = new Course
            {
                Name = "Math",
                Description = "Basic Math Course",
                Capacity = 30
            };

            await _context.Students.AddAsync(student);
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            var enrollment = new Enrollment
            {
                StudentId = student.Id,
                CourseId = course.Id,
                EnrollmentDate = System.DateTime.Now
            };

            await _enrollmentRepository.AddAsync(enrollment);

            var result = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.StudentId == student.Id && e.CourseId == course.Id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task EnrollmentRepository_GetAllAsync_ShouldReturnAllEnrollments()
        {
            var student = new Student
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            var course = new Course
            {
                Name = "Math",
                Description = "Basic Math Course",
                Capacity = 30
            };

            await _context.Students.AddAsync(student);
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            var enrollment = new Enrollment
            {
                StudentId = student.Id,
                CourseId = course.Id,
                EnrollmentDate = System.DateTime.Now
            };

            await _context.Enrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();

            var enrollments = await _enrollmentRepository.GetAllAsync();

            Assert.AreEqual(1, enrollments.Count);
        }
    }
}
