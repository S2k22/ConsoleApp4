namespace StudentEnrollmentSystem.Domain.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }

        // Navigation property for relationships
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}