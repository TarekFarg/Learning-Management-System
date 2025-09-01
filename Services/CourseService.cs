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
                Message = "Done",
                Id = course.Id
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
                Titel= course.Titel,
                Id = course.Id
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
                Titel = course.Titel,
                Id = course.Id
            };
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var list = await _context.Courses.ToListAsync();

            var result = new List<CourseDto>();
            foreach (var course in list)
            {
                result.Add(new CourseDto
                {
                    Succeeded = true,
                    Describtion = course.Describtion,
                    Titel = course.Titel,
                    InstructorId = course.InstructorId,
                    Message = "Done",
                    Id = course.Id
                });
            }
            return result;
        }

        public async Task<CourseDto> GetCourseByIdAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
                return new CourseDto { Message = $"courseId: {id} not found!" };

            return new CourseDto
            {
                Succeeded = true,
                Describtion = course.Describtion,
                Titel = course.Titel,
                InstructorId = course.InstructorId,
                Message = "Done",
                Id = course.Id
            };
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesByInstructorIdAsync(string id)
        {
            if (await _userManager.FindByIdAsync(id) == null)
                return new List<CourseDto> { new CourseDto { Message = $"InstructorId: {id} not found!" } };

            var list = await _context.Courses.Where(c => c.InstructorId == id).ToListAsync();

            var result = new List<CourseDto>();

            foreach (var course in list)
            {
                result.Add(new CourseDto
                {
                    Succeeded = true,
                    Describtion = course.Describtion,
                    Titel = course.Titel,
                    InstructorId = course.InstructorId,
                    Message = "Done",
                    Id = course.Id
                });
            }

            return result;
        }
    }
}
