using LearningManagementSystem.Dtos;

namespace LearningManagementSystem.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(string id);

        Task<UserDto> UpdateUserAsync(RigesterDto dto, string userID);

        Task<UserDto> DeleteUserAsync(string id);


        Task<String> AddRoleAsync(AddRoleDto dto);

        Task<IEnumerable<string>> GetUserRolesAsync(string UserId);

        Task<String> DeleteRoleAsync(string RoleName, string UserId);
    }
}
