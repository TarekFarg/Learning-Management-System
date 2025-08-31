using LearningManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.Dtos
{
    public class EnrollmentDto
    {
        public int CourseId { get; set; }
        public string StudentId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; }

        public bool Succeeded { get; set; }
    }
}
