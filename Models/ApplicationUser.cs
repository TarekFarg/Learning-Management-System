using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }


        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Course> CoursesCreated { get; set; } 
        public ICollection<AssignmentSubmission> AssignmentSubmissions { get; set; }
    }
}
