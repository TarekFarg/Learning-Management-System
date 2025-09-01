using LearningManagementSystem.Dtos;

namespace LearningManagementSystem.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(string id);

        Task<UserDto> UpdateUserAsync(RigesterDto dto, string userID);

        Task<UserDto> DeleteUserAsync(string id);
    }
}
