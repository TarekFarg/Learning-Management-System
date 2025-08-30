using LearningManagementSystem.Dtos;
using LearningManagementSystem.Models;

namespace LearningManagementSystem.Services
{
    public interface ICourseService
    {
        Task<CourseDto> CreateCourseAsync(CreateCourseDto dto);
        Task<CourseDto> UpdateCourseAsync(CreateCourseDto dto,int id);

        Task<CourseDto> DeleteCourseAsync(int id);

        Task<IEnumerable<Course>> GetAllCoursesAsync();

        Task<Course> GetCourseByIdAsync(int id);

        Task<IEnumerable<Course>> GetCoursesByInstructorIdAsync(string id);

    }
}
