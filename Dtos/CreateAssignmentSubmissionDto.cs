using LearningManagementSystem.Models;

namespace LearningManagementSystem.Dtos
{
    public class CreateAssignmentSubmissionDto
    {
        public int AssignmentId { get; set; }
        public string StudentId { get; set; }
    }
}
