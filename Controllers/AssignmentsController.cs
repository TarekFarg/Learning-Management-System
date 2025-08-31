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
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;
        public AssignmentsController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpGet("assignment{id}")]
        public async Task<IActionResult> GetAssignmentAsync(int id)
        {
            var result = await _assignmentService.GetAssignmentByIdAsync(id);

            if (!result.Succeeded)
                return BadRequest(result.Status);

            return Ok(result);
        }

        [HttpGet("courseAssignments{courseId}")]
        public async Task<IActionResult> GetAssignmentsByCourseId(int courseId)
        {
            var result = await _assignmentService.GetAssignmentsByCourseIdAsync(courseId);

            if (!result.Any())
                return BadRequest();

            if (!result.ElementAt(0).Succeeded)
            {
                if (result.ElementAt(0).CourseId == courseId)
                    return Ok(result.ElementAt(0).Status);

                return BadRequest(result.ElementAt(0).Status);
            }

            return Ok(result);

        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAssignmentAsync([FromBody] CreateAssignmentDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _assignmentService.CreateAssignmentAsync(dto);

            if(!result.Succeeded)
                return BadRequest(result.Status);

            return Ok(result);
        }


        [HttpPut("update{assignmentId}")]
        public async Task<IActionResult> UpdateAssignmentAsync([FromBody]CreateAssignmentDto dto , int assignmentId)
        {
            var result = await _assignmentService.UpdateAssignmentAsync(dto, assignmentId);

            if(!result.Succeeded)
                return BadRequest(result.Status);
            
            return Ok(result);
        }


        [HttpDelete("delete{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            var result = await _assignmentService.DeleteAssignmentAsync(id);

            if (!result.Succeeded)
                return BadRequest(result.Status);

            return Ok(result);
        }


    }
}
