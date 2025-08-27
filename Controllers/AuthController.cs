using LearningManagementSystem.Dtos;
using LearningManagementSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Rigester")]
        public async Task<IActionResult> RigesterAsync([FromBody]RigesterDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var resutl = await _authService.RigesterAsync(dto);

            if(!resutl.IsAuthenticated)
                return BadRequest(resutl.Message);

            return Ok(resutl);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] GetTokenDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resutl = await _authService.GetTokenAsync(dto);

            if (!resutl.IsAuthenticated)
                return BadRequest(resutl.Message);

            return Ok(resutl);

        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resutl = await _authService.AddRoleAsync(dto);

            if (!string.IsNullOrEmpty(resutl))
                return BadRequest(resutl);

            return Ok(dto);

        }

    }
}
