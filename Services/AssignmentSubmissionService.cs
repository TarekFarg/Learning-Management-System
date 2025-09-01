using Humanizer;
using LearningManagementSystem.Data;
using LearningManagementSystem.Dtos;
using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Services
{
    public class AssignmentSubmissionService : IAssignmentSubmissionService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AssignmentSubmissionService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }        

        public async Task<AssignmentSubmissionDto> CreateAssignmentSubmissionAsync(CreateAssignmentSubmissionDto dto)
        {
            if(await _context.Assignments.FindAsync(dto.AssignmentId) == null)
                return new AssignmentSubmissionDto { Status = $"AssignmentId: {dto.AssignmentId} NotFound!" };

            if(await _userManager.FindByIdAsync(dto.StudentId) == null)
                return new AssignmentSubmissionDto { Status = $"StudentId: {dto.StudentId} NotFound!" };

            var assSubmission = new AssignmentSubmission
            {
                StudentId = dto.StudentId,
                AssignmentId = dto.AssignmentId
            };

            await _context.AssignmentSubmissions.AddAsync(assSubmission);
            _context.SaveChanges();

            return new AssignmentSubmissionDto
            {
                Succeeded = true,
                Status = assSubmission.IsCorrected ? "Corrected" : "Not Corrected",
                AssignmentId = assSubmission.AssignmentId,
                StudentId = assSubmission.StudentId,
                SubmittedAt = assSubmission.SubmittedAt,
                id = assSubmission.id,
                Grade = assSubmission.Grade
            };
        }



        public async Task<AssignmentSubmissionDto> GetAssignmentSubmissionByIDAsync(int id)
        {
            var assSubmission = await _context.AssignmentSubmissions.FindAsync(id);

            if (assSubmission == null)
                return new AssignmentSubmissionDto { Status = $"AssignmentSubmissionId:{id} NotFound!" };

            return new AssignmentSubmissionDto
            {
                Succeeded = true,
                Status = assSubmission.IsCorrected ? "Corrected" : "Not Corrected" ,
                AssignmentId = assSubmission.AssignmentId ,
                StudentId = assSubmission.StudentId ,
                SubmittedAt = assSubmission.SubmittedAt ,
                id = assSubmission.id ,
                Grade = assSubmission.Grade
            };
        }

        public async Task<IEnumerable<AssignmentSubmissionDto>> GetAssignmentSubmissionsByAssIdAsync(int assId)
        {
            if (await _context.Assignments.FindAsync(assId) == null)
                return new List<AssignmentSubmissionDto>{
                    new AssignmentSubmissionDto{ Status = $"AssignmentId: {assId} NotFound!"}
                };

            var list = await _context.AssignmentSubmissions.Where(a => a.AssignmentId == assId).ToListAsync();

            if (!list.Any())
                return new List<AssignmentSubmissionDto>{
                    new AssignmentSubmissionDto{ Status = $"No submissions for this assignments"}
                };

            var result = new List<AssignmentSubmissionDto>();

            foreach(var assSubmission in  list)
            {
                result.Add(new AssignmentSubmissionDto
                {
                    Succeeded = true,
                    Status = assSubmission.IsCorrected ? "Corrected" : "Not Corrected",
                    AssignmentId = assSubmission.AssignmentId,
                    StudentId = assSubmission.StudentId,
                    SubmittedAt = assSubmission.SubmittedAt,
                    id = assSubmission.id,
                    Grade = assSubmission.Grade
                });
            }

            return result;
        }

        public async Task<AssignmentSubmissionDto> DeleteAssignmentSubmissionAsync(int id)
        {
            var assSubmission = await _context.AssignmentSubmissions.FindAsync(id);

            if (assSubmission == null)
                return new AssignmentSubmissionDto
                { Status = $"AssignmentSubmissionId : {id}" };

            _context.AssignmentSubmissions.Remove(assSubmission);
            _context.SaveChanges();

            return new AssignmentSubmissionDto
            {
                Succeeded = true,
                Status = "Deleted",
                AssignmentId = assSubmission.AssignmentId,
                StudentId = assSubmission.StudentId,
                SubmittedAt = assSubmission.SubmittedAt,
                id = assSubmission.id,
                Grade = assSubmission.Grade
            };
        }

        public async Task<AssignmentSubmissionDto> CorrectAssignmentSubmissionAsync(CorrectAssignmentSubmissionDto dto)
        {
            var assSubmission = await _context.AssignmentSubmissions.FindAsync(dto.AssignmentSubmissionId);

            if (assSubmission == null)
                return new AssignmentSubmissionDto
                { Status = $"AssignmentSubmissionId : {dto.AssignmentSubmissionId}" };

            assSubmission.Grade = dto.Grade;
            assSubmission.IsCorrected = true;

            _context.AssignmentSubmissions.Update(assSubmission);
            _context.SaveChanges(); 

            return new AssignmentSubmissionDto
            {
                Succeeded = true,
                Status = assSubmission.IsCorrected ? "Corrected" : "Not Corrected",
                AssignmentId = assSubmission.AssignmentId,
                StudentId = assSubmission.StudentId,
                SubmittedAt = assSubmission.SubmittedAt,
                id = assSubmission.id,
                Grade = assSubmission.Grade
            };
        }
    }
}
