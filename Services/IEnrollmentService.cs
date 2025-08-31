using LearningManagementSystem.Dtos;

namespace LearningManagementSystem.Services
{
    public interface IEnrollmentService
    {
        Task<EnrollmentDto> EnrollAsync(CreateEnrollmentDto dto);

        Task<IEnumerable<EnrollmentDto>> GetStudentEnrollmentsAsync(String StudentId);

        Task<IEnumerable<EnrollmentDto>> GetCourseEnrollmentsAsync(int CourseId);

        Task<EnrollmentDto> GetEnrollmentByIdAsync(int EnrollmentId);

        Task<EnrollmentDto> DeleteEnrollmentAsync(int  EnrollmentId);
    }
}
