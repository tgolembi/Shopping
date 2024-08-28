using Shopping.Services.AuthAPI.Models.DTO;

namespace Shopping.Services.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register (RegistrationDTO registrationDTO);

        Task<LoginResponseDTO> Login (LoginRequestDTO loginRequestDTO);
    }
}
