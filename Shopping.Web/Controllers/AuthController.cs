using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Shopping.Web.Enumerators;
using Shopping.Web.Models;
using Shopping.Web.Service.IService;
using Shopping.Web.Tools;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Shopping.Web.Controllers
{
	public class AuthController : Controller
	{
		private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

		public AuthController (IAuthService authService, ITokenProvider tokenProvider)
		{
			_authService = authService;
            _tokenProvider = tokenProvider;
		}

		[HttpGet]
		public IActionResult Login ()
		{
			LoginRequestDTO login = new();
			return View(login);
		}

        [HttpPost]
        public async Task<IActionResult> Login (LoginRequestDTO login)
        {
            ResponseDTO response = await _authService.LoginAsync(login);

            if (response != null && response.Success)
            {
                LoginResponseDTO? loginResponse = JsonHelper.Deserialize<LoginResponseDTO>(response?.Result?.ToString());

                await SignInUser(loginResponse);
                _tokenProvider.SetToken(loginResponse.Token);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("CustomError", response?.Message);
                 return View(login);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.RoleList = StaticDetails.GetEnumSelectList<StaticDetails.Role>();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register (RegistrationDTO registration)
        {
			ResponseDTO result = await _authService.RegisterAsync(registration);

			if (result != null && result.Success)
			{
				if (string.IsNullOrWhiteSpace(registration.Role))
				{
					registration.Role = StaticDetails.Role.Customer.ToString();
				}
				ResponseDTO assignRole = await _authService.AssignRoleAsync(registration);
				if (assignRole != null && assignRole.Success)
				{
					TempData["success"] = "Registration Successful";
					return RedirectToAction(nameof(Login));
				}
			}

            ViewBag.RoleList = StaticDetails.GetEnumSelectList<StaticDetails.Role>();

            return View(registration);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }


        private async Task SignInUser (LoginResponseDTO loginResponse)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(loginResponse.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
