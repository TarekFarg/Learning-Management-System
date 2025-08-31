using LearningManagementSystem.Data;
using LearningManagementSystem.Dtos;
using LearningManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly ApplicationDbContext _context;

        public AssignmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AssignmentDto> CreateAssignmentAsync(CreateAssignmentDto dto)
        {
            if (await _context.Courses.FindAsync(dto.CourseId) == null)
                return new AssignmentDto { Status = $"courseId: {dto.CourseId} not found!" };

            var assignment = new Assignment
            {
                CourseId = dto.CourseId,
                DueDate = dto.DueDate,
                Title = dto.Title,
            };

            await _context.AddAsync(assignment);
            _context.SaveChanges();

            return new AssignmentDto
            {
                Succeeded = true,
                Status = "Active",
                CourseId = assignment.CourseId,
                DueDate = assignment.DueDate,
                Title = assignment.Title,
                CreatedAt = assignment.CreatedAt
            };
        }

        public async Task<AssignmentDto> DeleteAssignmentAsync(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            
            if (assignment == null)
                return new AssignmentDto { Status = $"AssignmentId: {id} not found!" };

            _context.Assignments.Remove(assignment);
            _context.SaveChanges();

            return new AssignmentDto
            {
                Succeeded = true,
                CourseId = assignment.CourseId,
                CreatedAt = assignment.CreatedAt,
                DueDate = assignment.DueDate,
                Status = "Deleted",
                Title = assignment.Title
            };
        }

        public async Task<IEnumerable<AssignmentDto>> GetAssignmentsByCourseIdAsync(int courseId)
        {
            if(await _context.Courses.FindAsync(courseId) == null)
                return new List<AssignmentDto> { new AssignmentDto{Status = "CourseId not found!" } };

            var list = await _context.Assignments.Where(a => a.CourseId == courseId).ToListAsync();

            if(list.Count == 0)
                return new List<AssignmentDto> { new AssignmentDto 
                { 
                    Status = "No Assignments for this course!",
                    CourseId = courseId
                } };

            var result = new List<AssignmentDto>();

            foreach(var assignment in list)
            {
                result.Add(
                    new AssignmentDto{
                    CourseId = assignment.CourseId,
                    CreatedAt = assignment.CreatedAt,
                    DueDate = assignment.DueDate,
                    Succeeded = true,
                    Title = assignment.Title,
                    Status = "Active"
                });
            }

            return result;

        }

        public async Task<AssignmentDto> GetAssignmentByIdAsync(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);

            if (assignment == null)
                return new AssignmentDto { Status = $"AssignmentId:{id} not found!" };

            return new AssignmentDto
            {
                Succeeded = true,
                Title = assignment.Title,
                CreatedAt = assignment.CreatedAt,
                DueDate = assignment.DueDate,
                CourseId = assignment.CourseId,
                Status = "Active"
            };
        }

        public async Task<AssignmentDto> UpdateAssignmentAsync(CreateAssignmentDto dto, int assignmentId)
        {
            var assignment = await _context.Assignments.FindAsync(assignmentId);

            if (assignment == null)
                return new AssignmentDto { Status = $"AssignmentId: {assignmentId} Not found!" };

            if (await _context.Courses.FindAsync(dto.CourseId) == null)
                return new AssignmentDto { Status = $"courseId: {dto.CourseId} not found!" };

            assignment.CourseId = dto.CourseId;
            assignment.Title = dto.Title;
            assignment.DueDate = dto.DueDate;

            _context.Assignments.Update(assignment);
            _context.SaveChanges();

            return new AssignmentDto
            {
                Succeeded = true,
                Status = "Active",
                Title = assignment.Title,
                CourseId=assignment.CourseId,
                CreatedAt=assignment.CreatedAt,
                DueDate=assignment.DueDate,
            };
        }
    }
}
