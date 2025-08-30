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
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        [HttpGet("AllCourses")]
        public async Task<IActionResult> GetAllCourses()
        {
            return Ok(await _courseService.GetAllCoursesAsync());
        }


        [HttpGet("CourseID{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var result = await _courseService.GetCourseByIdAsync(id);

            if(result == null) 
                return NotFound($"id: {id} NotFound!");

            return Ok(result);
        }


        [HttpGet("InstructorId{id}")]
        public async Task<IActionResult> GetCoursesByInstructorId(string id)
        {
            // need to check if this id is constructor or not (use user service)
            var result = await _courseService.GetCoursesByInstructorIdAsync(id);
            return Ok(result);
        }


        [HttpPost("CreateCourse")]
        [Authorize(Roles = "Instructor,Admin")]
        public async Task<IActionResult> CreateAsync([FromBody]CreateCourseDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _courseService.CreateCourseAsync(dto);

            if(!result.Succeeded)
                return NotFound(result.Message);

            return Ok(result);
        }


        [HttpPut("UpdateCourse{id}")]
        [Authorize(Roles = "Instructor,Admin")]
        public async Task<IActionResult> UpdateAsync([FromBody] CreateCourseDto dto,int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _courseService.UpdateCourseAsync(dto,id);

            if (!result.Succeeded)
                return NotFound(result.Message);

            return Ok(result);
        }


        [HttpDelete("DeleteCourse{id}")]
        [Authorize(Roles = "Instructor,Admin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _courseService.DeleteCourseAsync(id);

            if(!result.Succeeded)
                return NotFound(result.Message);

            return Ok(result);
        }
    }
}
