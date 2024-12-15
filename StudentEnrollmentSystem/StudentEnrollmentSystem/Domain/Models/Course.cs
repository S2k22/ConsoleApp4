namespace StudentEnrollmentSystem.Domain.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }

        // Navigation property for relationships
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}