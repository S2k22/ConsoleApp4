using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentEnrollmentSystem.Domain.Models;
using StudentEnrollmentSystem.Domain.Validators;

namespace StudentEnrollmentSystem.Tests
{
    [TestClass]
    public class ValidatorsTests
    {
        [TestMethod]
        public void ValidateStudent_ShouldReturnFalseForInvalidEmail()
        {
            // Arrange
            var student = new Student
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "invalidemail"
            };

            // Act
            var result = StudentValidator.ValidateStudent(student, out var errorMessage);

            // Assert
            Assert.IsFalse(result, "Validation should fail for an invalid email.");
            Assert.AreEqual("Valid email address is required.", errorMessage, "Error message should indicate the email is invalid.");
        }
    }
}