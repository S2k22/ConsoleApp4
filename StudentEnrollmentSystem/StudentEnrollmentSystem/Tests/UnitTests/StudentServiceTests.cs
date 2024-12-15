using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudentEnrollmentSystem.Data;
using StudentEnrollmentSystem.Domain.Models;
using StudentEnrollmentSystem.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentEnrollmentSystem.Tests.UnitTests
{
    [TestClass]
    public class StudentServiceTests
    {
        private Mock<StudentRepository> _mockRepository;
        private StudentService _studentService;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<StudentRepository>(null);
            _studentService = new StudentService(_mockRepository.Object);
        }

        [TestMethod]
        public async Task AddStudent_ShouldCallRepositoryOnce()
        {
            // Arrange
            var student = new Student
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(2000, 1, 1),
                Email = "john.doe@example.com"
            };

            // Act
            await _studentService.AddStudentAsync(student);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(student), Times.Once);
        }

        [TestMethod]
        public async Task GetAllStudents_ShouldReturnAllStudents()
        {
            // Arrange
            var students = new List<Student>
            {
                new Student { Id = 1, FirstName = "John", LastName = "Doe" },
                new Student { Id = 2, FirstName = "Jane", LastName = "Smith" }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(students);

            // Act
            var result = await _studentService.GetAllStudentsAsync();

            // Assert
            Assert.AreEqual(2, result.Count);
        }
    }
}