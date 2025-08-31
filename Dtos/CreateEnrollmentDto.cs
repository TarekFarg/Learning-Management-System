using LearningManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.Dtos
{
    public class CreateEnrollmentDto
    {
        public int CourseId { get; set; }
        public string StudentId { get; set; }
    }
}
