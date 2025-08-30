namespace LearningManagementSystem.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Titel { get; set; }
        public string Describtion { get; set; }

        public String InstructorId { get; set; }
        public ApplicationUser Instructor { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
    }
}
