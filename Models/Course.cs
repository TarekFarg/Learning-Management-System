using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.Models
{
    public class Course
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Titel { get; set; }
        [MaxLength(300)]
        public string Describtion { get; set; }

        public String InstructorId { get; set; }
        public ApplicationUser Instructor { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
    }
}
