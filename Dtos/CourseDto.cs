using LearningManagementSystem.Models;

namespace LearningManagementSystem.Dtos
{
    public class CourseDto
    {
        public string Titel { get; set; }
        public string Describtion { get; set; }
        public String InstructorId { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
