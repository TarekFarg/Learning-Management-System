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
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet("courseEnrollments/{courseId}")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> GetCourseEnrollments(int courseId)
        {
            var result = await _enrollmentService.GetCourseEnrollmentsAsync(courseId);

            if (!result.Any())
                return Ok("No Enrollments for this course");

            if (!result.ElementAt(0).Succeeded)
                return BadRequest(result.ElementAt(0).Status);

            return Ok(result);
        }


        [HttpGet("studentEnrollments/{StudentId}")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> GetStudentEnrollments(string StudentId)
        {
            var result = await _enrollmentService.GetStudentEnrollmentsAsync(StudentId);

            if (!result.Any())
                return Ok("No Enrollments for this Student");

            if (!result.ElementAt(0).Succeeded)
                return BadRequest(result.ElementAt(0).Status);

            return Ok(result);
        }


        [HttpGet("enrollment/{id}")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> GetEnrollmentById(int id)
        {
            var result = await _enrollmentService.GetEnrollmentByIdAsync(id);

            if (!result.Succeeded)
                return BadRequest(result.Status);

            return Ok(result);
        }


        [HttpPost("Enroll")]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> EnrollAsync([FromBody] CreateEnrollmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _enrollmentService.EnrollAsync(dto);

            if (!result.Succeeded)
                return BadRequest(result.Status);

            return Ok(result);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(int id )
        {
            var result = await _enrollmentService.DeleteEnrollmentAsync(id);

            if(!result.Succeeded)
                return BadRequest(result.Status);

            return Ok(result);
        }
    }
}
