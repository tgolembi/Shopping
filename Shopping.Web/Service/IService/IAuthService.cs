using Shopping.Web.Models;

namespace Shopping.Web.Service.IService
{
	public interface IAuthService
	{
		Task<ResponseDTO?> LoginAsync (LoginRequestDTO loginDTO);
		Task<ResponseDTO?> RegisterAsync (RegistrationDTO registrationDTO);
		Task<ResponseDTO?> AssignRoleAsync (RegistrationDTO registrationDTO);
	}
}
