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
    public class CourseServiceTests
    {
        private Mock<CourseRepository> _mockRepository;
        private CourseService _courseService;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<CourseRepository>(null);
            _courseService = new CourseService(_mockRepository.Object);
        }

        [TestMethod]
        public async Task AddCourse_ShouldCallRepositoryOnce()
        {
            // Arrange
            var course = new Course
            {
                Name = "Mathematics",
                Description = "Basic Math Course",
                Capacity = 30
            };

            // Act
            await _courseService.AddCourseAsync(course);

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(course), Times.Once);
        }

        [TestMethod]
        public async Task GetAllCourses_ShouldReturnAllCourses()
        {
            // Arrange
            var courses = new List<Course>
            {
                new Course { Id = 1, Name = "Math" },
                new Course { Id = 2, Name = "Science" }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(courses);

            // Act
            var result = await _courseService.GetAllCoursesAsync();

            // Assert
            Assert.AreEqual(2, result.Count);
        }
    }
}