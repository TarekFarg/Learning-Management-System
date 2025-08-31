using LearningManagementSystem.Models;

namespace LearningManagementSystem.Dtos
{
    public class AssignmentDto
    {     
        public int CourseId { get; set; }        
        public string Title { get; set; }

        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }   
        
        public bool Succeeded { get; set; }

        public string Status { get; set; }
    }
}
