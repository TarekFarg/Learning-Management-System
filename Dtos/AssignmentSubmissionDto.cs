using LearningManagementSystem.Models;

namespace LearningManagementSystem.Dtos
{
    public class AssignmentSubmissionDto
    {
        public int id { get; set; }

        public int AssignmentId { get; set; }

        public string StudentId { get; set; }
        public double Grade { get; set; }

        public string Status { get; set; }

        public bool Succeeded { get; set; }

        public DateTime SubmittedAt { get; set; }
    }
}
