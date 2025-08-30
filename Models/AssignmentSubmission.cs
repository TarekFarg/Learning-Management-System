namespace LearningManagementSystem.Models
{
    public class AssignmentSubmission
    {
        public int id {  get; set; }

        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }


        public string StudentId { get; set; }
        public ApplicationUser Student { get; set; }

        public double Grade { get; set; }

        public Boolean IsCorrected { get; set; }

        public DateTime SubmittedAt { get; set; } 
    }
}
