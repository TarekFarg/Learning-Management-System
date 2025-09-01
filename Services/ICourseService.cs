using LearningManagementSystem.Dtos;
using LearningManagementSystem.Models;

namespace LearningManagementSystem.Services
{
    public interface ICourseService
    {
        Task<CourseDto> CreateCourseAsync(CreateCourseDto dto);
        Task<CourseDto> UpdateCourseAsync(CreateCourseDto dto,int id);

        Task<CourseDto> DeleteCourseAsync(int id);

        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();

        Task<CourseDto> GetCourseByIdAsync(int id);

        Task<IEnumerable<CourseDto>> GetCoursesByInstructorIdAsync(string id);

    }
}
