using Microsoft.AspNetCore.Identity;

namespace LearningManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Course> CoursesCreated { get; set; } 
        public ICollection<AssignmentSubmission> AssignmentSubmissions { get; set; }
    }
}
