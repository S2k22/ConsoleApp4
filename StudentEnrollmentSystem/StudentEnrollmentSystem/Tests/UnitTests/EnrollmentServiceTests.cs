using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudentEnrollmentSystem.Data;
using StudentEnrollmentSystem.Domain.Models;
using StudentEnrollmentSystem.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentEnrollmentSystem.Tests.UnitTests
{
    [TestClass]
    public class EnrollmentServiceTests
    {
        private Mock<EnrollmentRepository> _mockRepository;
        private EnrollmentService _enrollmentService;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<EnrollmentRepository>(null);
            _enrollmentService = new EnrollmentService(_mockRepository.Object);
        }

        [TestMethod]
        public async Task AddEnrollment_ShouldCallRepositoryOnce()
        {
            // Arrange
            var enrollment = new Enrollment
            {
                StudentId = 1,
                CourseId = 2,
                EnrollmentDate = DateTime.Now
            };

            // Act
            await _enrollmentService.AddEnrollmentAsync(enrollment);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(enrollment), Times.Once);
        }

        [TestMethod]
        public async Task GetAllEnrollments_ShouldReturnAllEnrollments()
        {
            // Arrange
            var enrollments = new List<Enrollment>
            {
                new Enrollment { StudentId = 1, CourseId = 1 },
                new Enrollment { StudentId = 2, CourseId = 2 }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(enrollments);

            // Act
            var result = await _enrollmentService.GetAllEnrollmentsAsync();

            // Assert
            Assert.AreEqual(2, result.Count);
        }
    }
}