using LearningManagementSystem.Dtos;
using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop.Infrastructure;

namespace LearningManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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
                Status = "Updated",
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
                Status = "Deleted",
                Succeeded = true,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                UserName = user.UserName
            };
        }


        public async Task<string> AddRoleAsync(AddRoleDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);

            if (user == null)
                return "User Id is incorrect!";

            if (await _roleManager.FindByNameAsync(dto.Role) == null)
                return "RoleName is in correct!";

            if (await _userManager.IsInRoleAsync(user, dto.Role))
                return "User already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, dto.Role);

            return result.Succeeded ? string.Empty : "Sonething went wrong";
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);

            if (user == null)
                return new List<string>();
            
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<string> DeleteRoleAsync(string RoleName, string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);

            if (user == null)
                return $"userId: {UserId} not found!";

            if(await _roleManager.FindByNameAsync(RoleName) == null)
                return $"roleName: {RoleName} Not found!";

            var result = await _userManager.RemoveFromRoleAsync(user, RoleName);


            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                    errors += $"{error}, ";

                return errors;
            }

            return string.Empty;
        }
    }
}
