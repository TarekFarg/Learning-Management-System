using LearningManagementSystem.Data;
using LearningManagementSystem.Dtos;
using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EnrollmentService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<EnrollmentDto> EnrollAsync(CreateEnrollmentDto dto)
        {
            if (await _context.Courses.FindAsync(dto.CourseId) == null)
                return new EnrollmentDto { Status = $"CourseId:{dto.CourseId} NotFound!"};

            var user = await _userManager.FindByIdAsync(dto.StudentId);
            if (user == null || !await _userManager.IsInRoleAsync(user, "Student"))
                return new EnrollmentDto { Status =$"UserId:{dto.StudentId} is incorrect!" };

            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
            };

            await _context.Enrollments.AddAsync(enrollment);
            _context.SaveChanges();

            return new EnrollmentDto
            {
                CourseId = enrollment.CourseId,
                StudentId = enrollment.StudentId,
                EnrollmentDate = enrollment.EnrollmentDate,
                Status = enrollment.Status,
                Succeeded = true
            };
        }

        public async Task<IEnumerable<EnrollmentDto>> GetCourseEnrollmentsAsync(int CourseId)
        {
            if(await _context.Courses.FindAsync(CourseId) == null)
                return new List<EnrollmentDto> { new EnrollmentDto { Status = $"courseId NotFound" } };

            var list = await _context.Enrollments.Where(e => e.CourseId ==  CourseId).ToListAsync();
      
            var result = new List<EnrollmentDto>();
            
            foreach(var item in list)
            {
                var current = new EnrollmentDto
                {
                    CourseId = item.CourseId,
                    StudentId = item.StudentId,
                    EnrollmentDate = item.EnrollmentDate,
                    Status = item.Status,
                    Succeeded = true
                };
                result.Add(current);
            }
            
            return result;
        }

        public async Task<EnrollmentDto> GetEnrollmentByIdAsync(int EnrollmentId)
        {
            var enrollment = await _context.Enrollments.FindAsync(EnrollmentId);

            if (enrollment == null)
                return new EnrollmentDto { Status = "EnrollmentId notFound!" };

            return new EnrollmentDto
            {
                Succeeded = true,
                Status = enrollment.Status,
                CourseId=enrollment.CourseId,
                StudentId=enrollment.StudentId,
                EnrollmentDate = enrollment.EnrollmentDate
            };
        }

        public async Task<IEnumerable<EnrollmentDto>> GetStudentEnrollmentsAsync(string StudentId)
        {
            var user = await _userManager.FindByIdAsync(StudentId);
            if (user == null)
                return new List<EnrollmentDto> { new EnrollmentDto { Status = $"StudentId NotFound" } };

            if(!await _userManager.IsInRoleAsync(user,"Student"))
                return new List<EnrollmentDto> { new EnrollmentDto { Status = $"This User is not Student!" } };


            var list = await _context.Enrollments.Where(e => e.StudentId == StudentId).ToListAsync();

            var result = new List<EnrollmentDto>();

            foreach (var item in list)
            {
                var current = new EnrollmentDto
                {
                    CourseId = item.CourseId,
                    StudentId = item.StudentId,
                    EnrollmentDate = item.EnrollmentDate,
                    Status = item.Status,
                    Succeeded = true
                };
                result.Add(current);
            }

            return result;
        }

        public async Task<EnrollmentDto> DeleteEnrollmentAsync(int EnrollmentId)
        {
            var enrollment = await _context.Enrollments.FindAsync(EnrollmentId);

            if (enrollment == null)
                return new EnrollmentDto { Status = "EnrollmentId notFound!" };

            _context.Enrollments.Remove(enrollment);

            return new EnrollmentDto
            {
                Succeeded = true,
                Status = enrollment.Status,
                CourseId = enrollment.CourseId,
                StudentId = enrollment.StudentId,
                EnrollmentDate = enrollment.EnrollmentDate
            };

        }

    }
}
