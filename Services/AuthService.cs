using LearningManagementSystem.Dtos;
using LearningManagementSystem.Helpers;
using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LearningManagementSystem.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
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

        public async Task<AuthDto> GetTokenAsync(GetTokenDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if(user == null || !await _userManager.CheckPasswordAsync(user,dto.Password))
                return new AuthDto { Message = "Email or password is incorrect!" };

            var token = await CreateJwtToken(user);
            
            var reoles = await _userManager.GetRolesAsync(user);

            return new AuthDto
            {
                Email = user.Email,
                IsAuthenticated = true,
                Username = user.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresOn = token.ValidTo,
                Roles = reoles.ToList()
            };
        }

        public async Task<AuthDto> RigesterAsync(RigesterDto dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) != null)
                return new AuthDto { Message = "this Email already exist!" };

            if(await _userManager.FindByNameAsync(dto.Username)  != null)
                return new AuthDto { Message = "this Username already exist!" };

            var user = new ApplicationUser
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.Username,
            };

            var result = await _userManager.CreateAsync(user,dto.Password);

            if(!result.Succeeded)
            {
                var errors = string.Empty;

                foreach(var error in result.Errors)
                    errors += $"{error.Description} , ";

                return new AuthDto { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, "User");

            var token = await CreateJwtToken(user);

            return new AuthDto
            {
                Email = user.Email,
                IsAuthenticated = true,
                Username = user.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresOn = token.ValidTo,
                Roles = new List<string> { "User" }
            };
        }


        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
