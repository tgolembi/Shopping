using Shopping.Web.Models;
using Shopping.Web.Enumerators;
using Shopping.Web.Service.IService;

namespace Shopping.Web.Service
{
	public class AuthService : IAuthService
	{
		private readonly IBaseService _baseService;

		public AuthService(IBaseService baseService)
		{
			_baseService = baseService;
		}

		public async Task<ResponseDTO?> AssignRoleAsync(RegistrationDTO registrationDTO)
		{
			return await _baseService.SendAsync(new RequestDTO()
			{
				ApiMethod = StaticDetails.ApiMethod.POST,
				Data = registrationDTO,
				Url = StaticDetails.AuthAPIBase + "/auth/assignrole"
			});
		}

		public async Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginDTO)
		{
			return await _baseService.SendAsync(new RequestDTO()
			{
				ApiMethod = StaticDetails.ApiMethod.POST,
				Data = loginDTO,
				Url = StaticDetails.AuthAPIBase + "/auth/login"
			}, withBearerToken: false);
		}

		public async Task<ResponseDTO?> RegisterAsync(RegistrationDTO registrationDTO)
		{
			return await _baseService.SendAsync(new RequestDTO()
			{
				ApiMethod = StaticDetails.ApiMethod.POST,
				Data = registrationDTO,
				Url = StaticDetails.AuthAPIBase + "/auth/register"
			}, withBearerToken: false);
		}
	}
}
