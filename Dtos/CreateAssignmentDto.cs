namespace LearningManagementSystem.Dtos
{
    public class CreateAssignmentDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; }

        public DateTime DueDate { get; set; }
    }
}
