using Microsoft.AspNetCore.Identity;
using Shopping.Services.AuthAPI.Data;
using Shopping.Services.AuthAPI.Models;
using Shopping.Services.AuthAPI.Models.DTO;
using Shopping.Services.AuthAPI.Service.IService;

namespace Shopping.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService (AppDbContext dbContext, 
                            IJwtTokenGenerator jwtTokenGenerator,
                            UserManager<ApplicationUser> userManager, 
                            RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<LoginResponseDTO> Login (LoginRequestDTO loginRequestDTO)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.Username.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDTO() { User = null, Token = string.Empty };
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            UserDTO userDTO = new()
            {
                ID = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                User = userDTO,
                Token = token
            };

            return loginResponseDTO;
        }

        public async Task<string?> Register (RegistrationDTO registrationDTO)
        {
            ApplicationUser user = new()
            {
                UserName = registrationDTO.Email,
                Email = registrationDTO.Email,
                NormalizedEmail = registrationDTO.Email.ToUpper(),
                FullName = registrationDTO.FullName,
                PhoneNumber = registrationDTO.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationDTO.Password);

                if (result.Succeeded)
                {
                    //var createdUser = _dbContext.ApplicationUsers.FirstOrDefault(user => user.Email == registrationDTO.Email);

                    return null;
                }
                else
                {
                    return result.Errors.FirstOrDefault()?.Description;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<bool> AssignRole (string email, string roleName)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
				}

                await _userManager.AddToRoleAsync(user, roleName);

                return true;
            }
            return false;
        }
    }
}
