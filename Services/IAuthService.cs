using LearningManagementSystem.Dtos;

namespace LearningManagementSystem.Services
{
    public interface IAuthService
    {
        Task<AuthDto> RigesterAsync(RigesterDto dto);

        Task<AuthDto> GetTokenAsync(GetTokenDto dto);

        
    }
}
