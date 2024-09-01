using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shopping.Web.Enumerators;
using Shopping.Web.Models;
using Shopping.Web.Service.IService;
using Shopping.Web.Tools;

namespace Shopping.Web.Controllers
{
	public class AuthController : Controller
	{
		private readonly IAuthService _authService;

		public AuthController (IAuthService authService)
		{
			_authService = authService;
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
        public IActionResult Logout()
        {
            return View();
        }
    }
}
