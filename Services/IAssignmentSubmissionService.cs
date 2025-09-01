using LearningManagementSystem.Dtos;

namespace LearningManagementSystem.Services
{
    public interface IAssignmentSubmissionService
    {
        Task<AssignmentSubmissionDto> CreateAssignmentSubmissionAsync(CreateAssignmentSubmissionDto dto);
        Task<AssignmentSubmissionDto> CorrectAssignmentSubmissionAsync(CorrectAssignmentSubmissionDto dto);
        Task<AssignmentSubmissionDto> GetAssignmentSubmissionByIDAsync(int id);
        Task<IEnumerable<AssignmentSubmissionDto>> GetAssignmentSubmissionsByAssIdAsync(int assId);
        Task<AssignmentSubmissionDto> DeleteAssignmentSubmissionAsync(int id);

    }
}
