using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using StudentEnrollmentSystem.Data;
using StudentEnrollmentSystem.Domain.Models;
using System.Threading.Tasks;

namespace StudentEnrollmentSystem.Tests.IntegrationTests
{
    [TestClass]
    public class DatabaseTests
    {
        private DatabaseContext _context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _context = new DatabaseContext(options);
        }

        [TestCleanup]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task AddStudent_ShouldStoreInDatabase()
        {
            // Arrange
            var student = new Student
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new System.DateTime(2000, 1, 1),
                Email = "john.doe@example.com"
            };

            // Act
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            var storedStudent = await _context.Students.FirstOrDefaultAsync(s => s.Email == "john.doe@example.com");

            // Assert
            Assert.IsNotNull(storedStudent, "Stored student should not be null.");
            Assert.AreEqual("John", storedStudent.FirstName, "First name should match.");
            Assert.AreEqual("Doe", storedStudent.LastName, "Last name should match.");
            Assert.AreEqual("john.doe@example.com", storedStudent.Email, "Email should match.");
        }
    }
}