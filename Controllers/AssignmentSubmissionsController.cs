using LearningManagementSystem.Dtos;
using LearningManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentSubmissionsController : ControllerBase
    {
        private readonly IAssignmentSubmissionService _assignmentSubmissionService;

        public AssignmentSubmissionsController(IAssignmentSubmissionService assignmentSubmissionService)
        {
            _assignmentSubmissionService = assignmentSubmissionService;
        }

        [HttpGet("submission{id}")]
        public async Task<IActionResult> GetAssignmentSubmissionByIdAsync(int id)
        {
            var result = await _assignmentSubmissionService.GetAssignmentSubmissionByIDAsync(id);

            if (!result.Succeeded)
                return BadRequest(result.Status);

            return Ok(result);
        }

        [HttpGet("assignment{assId}")]
        public async Task<IActionResult> GetAssignmentsSubmissionByAssIdAsync(int assId)
        {
            var result = await _assignmentSubmissionService.GetAssignmentSubmissionsByAssIdAsync(assId);

            if (result == null || !result.Any())
                return BadRequest();

            if(!result.ElementAt(0).Succeeded)
                return BadRequest(result.ElementAt(0).Status);

            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAssignmentSubmissionAsync
            ([FromBody] CreateAssignmentSubmissionDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _assignmentSubmissionService.CreateAssignmentSubmissionAsync(dto);

            if(!result.Succeeded)
                return BadRequest(result.Status);

            return Ok(result);
        }

        [HttpPut("correct")]
        public async Task<IActionResult> CorrectAssignmentSubmissionAsync
            ([FromBody] CorrectAssignmentSubmissionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _assignmentSubmissionService.CorrectAssignmentSubmissionAsync(dto);

            if(!result.Succeeded)
                return BadRequest(result.Status);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignmentsubmissionAsycn(int id)
        {
            var result = await _assignmentSubmissionService.DeleteAssignmentSubmissionAsync(id);

            if (!result.Succeeded)
                return BadRequest(result.Status);

            return Ok(result);
        }
    }
}
