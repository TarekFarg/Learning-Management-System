using LearningManagementSystem.Dtos;
using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop.Infrastructure;

namespace LearningManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return new UserDto { Status = $"UserId: {id} not found!" };

            return new UserDto
            {
                Status = "success",
                Succeeded = true,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                UserName = user.UserName
            };
        }

        public async Task<UserDto> UpdateUserAsync(RigesterDto dto, string userID)
        {
            var user = await _userManager.FindByIdAsync(userID);

            if (user == null)
                return new UserDto { Status = $"UserId: {userID} not found!" };

            user.Email = dto.Email;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.UserName = dto.Username;

            var result = await _userManager.UpdateAsync(user);
            
            if(!result.Succeeded)
            {
                string errors = string.Empty;
                foreach (var error in result.Errors)
                    errors += $"{error}, ";

                return new UserDto { Status=errors };
            }

            return new UserDto 
            {
                Status = "success",
                Succeeded = true,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                UserName = user.UserName
            };
        }

        public async Task<UserDto> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return new UserDto { Status = $"UserId: {id} not found!" };

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                string errors = string.Empty;
                foreach (var error in result.Errors)
                    errors += $"{error}, ";

                return new UserDto { Status = errors };
            }

            return new UserDto
            {
                Status = "success",
                Succeeded = true,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                UserName = user.UserName
            };
        }

    }
}
