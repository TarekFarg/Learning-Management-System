using LearningManagementSystem.Dtos;

namespace LearningManagementSystem.Services
{
    public interface IAssignmentService
    {
        // creata ass 
        Task<AssignmentDto> CreateAssignmentAsync(CreateAssignmentDto dto);

        // update ass 
        Task<AssignmentDto> UpdateAssignmentAsync(CreateAssignmentDto dto , int assignmentId);
        
        // delete ass
        Task<AssignmentDto> DeleteAssignmentAsync(int id);

        // get ass
        Task<AssignmentDto> GetAssignmentByIdAsync(int id);
        Task<IEnumerable<AssignmentDto>> GetAssignmentsByCourseIdAsync(int courseId);


    }
}
