using Microsoft.AspNetCore.Mvc;
using Shopping.Web.Models;
using Shopping.Web.Service.IService;

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

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
    }
}
