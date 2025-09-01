using LearningManagementSystem.Dtos;
using LearningManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {
            var result = await _userService.GetUserByIdAsync(id);

            if(!result.Succeeded)
                return BadRequest(result.Status);

            return Ok(result);
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUserByIdAsync([FromBody] RigesterDto dto,string id)
        {
            var result = await _userService.UpdateUserAsync(dto,id);

            if (!result.Succeeded)
                return BadRequest(result.Status);

            return Ok(result);
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (!result.Succeeded)
                return BadRequest(result.Status);

            return Ok(result);
        }

        // Roles

        [HttpGet("roles/{id}")]
        public async Task<IActionResult> GetUserRolesAsync(string id)
        {
            var result = await _userService.GetUserRolesAsync(id);

            if(result == null || !result.Any())
                return BadRequest($"UserId: {id} not found!");

            return Ok(result);
        }

        [HttpPost("addRole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resutl = await _userService.AddRoleAsync(dto);

            if (!string.IsNullOrEmpty(resutl))
                return BadRequest(resutl);

            return Ok(dto);

        }


        [HttpDelete("roles/delete/{userId}")]
        public async Task<IActionResult> DeleteRoleAsync(string userId, [FromBody] string roleName)
        {
            var result = await _userService.DeleteRoleAsync(roleName,userId);

            if (string.IsNullOrEmpty(result))
                return Ok("Deleted");

            return BadRequest(result);

        }

    }
}
