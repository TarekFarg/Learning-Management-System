using LearningManagementSystem.Data;
using LearningManagementSystem.Dtos;
using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace LearningManagementSystem.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        
        public CourseService(ApplicationDbContext context , UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<CourseDto> CreateCourseAsync(CreateCourseDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.InstructorId);
            if (user == null || !await _userManager.IsInRoleAsync(user, "Instructor"))
                return new CourseDto { Message = $"This Id notFound or not instructor!"};

            var course = new Course
            {
                Describtion = dto.Describtion,
                Titel = dto.Titel,
                InstructorId = dto.InstructorId,
            };


            await _context.Courses.AddAsync(course);
            _context.SaveChanges();
            return new CourseDto
            {
                Succeeded = true,
                Describtion = course.Describtion,
                Titel = course.Titel,
                InstructorId = course.InstructorId,
                Message = "Done"
            };
        }

        public async Task<CourseDto> UpdateCourseAsync(CreateCourseDto dto , int id)
        {
            var course = await _context.Courses.FindAsync(id);
            
            if (course == null)
                return new CourseDto { Message = $"This CourseId:{id} is incorrect!" };

            var user = await _userManager.FindByIdAsync(dto.InstructorId);
            if (user == null || !await _userManager.IsInRoleAsync(user, "Instructor"))
                return new CourseDto { Message = $"This Id notFound or not instructor!" };

            course.Describtion = dto.Describtion;
            course.Titel = dto.Titel;
            course.InstructorId = dto.InstructorId;

            _context.Courses.Update(course);
            _context.SaveChanges();

            return new CourseDto
            {
                Succeeded = true,
                Message = "Updated",
                InstructorId = course.InstructorId,
                Describtion = course.Describtion,
                Titel= course.Titel
            };
        }

        public async Task<CourseDto> DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            
            if ( course == null)
                return new CourseDto { Message = $"This id:{id} is not found!"} ;

            _context.Courses.Remove(course);
            _context.SaveChanges() ;

            return new CourseDto
            {
                Succeeded= true,
                Message = "Deleted",
                InstructorId = course.InstructorId,
                Describtion= course.Describtion,
                Titel = course.Titel
            };
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses
                .Include(c => c.Instructor)
                .ToListAsync();
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task<IEnumerable<Course>> GetCoursesByInstructorIdAsync(string id)
        {
            if(await _userManager.FindByIdAsync(id)== null)
                return Enumerable.Empty<Course>();

            return await _context.Courses.Where(c => c.InstructorId == id).ToListAsync();
        }
    }
}
